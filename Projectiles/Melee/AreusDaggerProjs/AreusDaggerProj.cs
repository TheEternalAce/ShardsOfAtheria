using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusDaggerProjs
{
    public class AreusDaggerProj : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/AreusDagger";

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();

            SoAGlobalProjectile.Metalic.Add(Type, 1f);
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(18); // This sets width and height to the same value (important when projectiles can rotate)
            Projectile.aiStyle = -1; // Use our own AI to customize how it behaves, if you don't want that, keep this at ProjAIStyleID.ShortSword. You would still need to use the code in SetVisualOffsets() though
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            if (SoA.ServerConfig.throwingWeapons) Projectile.DamageType = DamageClass.Throwing;
            Projectile.extraUpdates = 1; // Update 1+extraUpdates times per tick
            Projectile.timeLeft = 180; // This value does not matter since we manually kill it earlier, it just has to be higher than the duration we use in AI
        }

        // See ExampleBehindTilesProjectile. 
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Projectile.hide = true;
            behindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var point = (Projectile.Center / 16).ToPoint();
            lightColor = Lighting.GetColor(point);
            return base.PreDraw(ref lightColor);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center); // Play a death sound
            Vector2 usePos = Projectile.position; // Position to use for dusts

            // Please note the usage of MathHelper, please use this!
            // We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
            float rotation = 45f;
            if (Projectile.spriteDirection == -1)
            {
                rotation += 90;
            }
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(rotation)).ToRotationVector2(); // rotation vector to use for dust velocity

            usePos += rotVector * 16f;

            // Declaring a constant in-line is fine as it will be optimized by the compiler
            // It is however recommended to define it outside method scope if used elswhere as well
            // They are useful to make numbers that don't change more descriptive
            const int NUM_DUSTS = 20;

            // Spawn some dusts upon javelin death
            for (int i = 0; i < NUM_DUSTS; i++)
            {
                // Create a new dust
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }

        // Are we sticking to a target?
        public bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        // Index of the current target
        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        // Randomize projectile frame
        public bool frameSet = false;

        private const int MAX_STICKY_JAVELINS = 8; // This is the max. amount of javelins being able to attach
        private readonly Point[] _stickingJavelins = new Point[MAX_STICKY_JAVELINS]; // The point array holding for sticking javelins
        private int initialDamage;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            IsStickingToTarget = true; // we are sticking to a target
            TargetWhoAmI = target.whoAmI; // Set the target whoAmI
            Projectile.velocity =
                (target.Center - Projectile.Center) *
                0.75f; // Change velocity based on delta center of targets (difference between entity centers)
            Projectile.netUpdate = true; // netUpdate this javelin
            Projectile.timeLeft = 3600;

            initialDamage = damageDone;
            Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore

            // It is recommended to split your code into separate methods to keep code clean and clear
            UpdateStickyJavelins(target);
        }

        /*
		 * The following code handles the javelin sticking to the enemy hit.
		 */
        private void UpdateStickyJavelins(NPC target)
        {
            int currentJavelinIndex = 0; // The javelin index

            for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != Projectile.whoAmI // Make sure the looped projectile is not the current javelin
                    && currentProjectile.active // Make sure the projectile is active
                    && currentProjectile.owner == Main.myPlayer // Make sure the projectile's owner is the client's player
                    && currentProjectile.type == Projectile.type // Make sure the projectile is of the same type as this javelin
                    && currentProjectile.ModProjectile is AreusDaggerProj javelinProjectile // Use a pattern match cast so we can access the projectile like an ExampleJavelinProjectile
                    && javelinProjectile.IsStickingToTarget // the previous pattern match allows us to use our properties
                    && javelinProjectile.TargetWhoAmI == target.whoAmI)
                {

                    _stickingJavelins[currentJavelinIndex++] = new Point(i, currentProjectile.timeLeft); // Add the current projectile's index and timeleft to the point array
                    if (currentJavelinIndex >= _stickingJavelins.Length)  // If the javelin's index is bigger than or equal to the point array's length, break
                        break;
                }
            }

            // Remove the oldest sticky javelin if we exceeded the maximum
            if (currentJavelinIndex >= MAX_STICKY_JAVELINS)
            {
                int oldJavelinIndex = 0;
                // Loop our point array
                for (int i = 1; i < MAX_STICKY_JAVELINS; i++)
                {
                    // Remove the already existing javelin if it's timeLeft value (which is the Y value in our point array) is smaller than the new javelin's timeLeft
                    if (_stickingJavelins[i].Y < _stickingJavelins[oldJavelinIndex].Y)
                    {
                        oldJavelinIndex = i; // Remember the index of the removed javelin
                    }
                }
                // Remember that the X value in our point array was equal to the index of that javelin, so it's used here to kill it.
                Main.projectile[_stickingJavelins[oldJavelinIndex].X].Kill();
            }
        }

        // Added these 2 constant to showcase how you could make AI code cleaner by doing this
        // Change this number if you want to alter how long the javelin can travel at a constant speed
        private const int MAX_TICKS = 45;

        // Change this number if you want to alter how the alpha changes
        private const int ALPHA_REDUCTION = 25;

        public override void AI()
        {
            UpdateAlpha();
            // Run either the Sticky AI or Normal AI
            // Separating into different methods helps keeps your AI clean
            Projectile.SetVisualOffsets(new Vector2(48, 54));
            if (IsStickingToTarget) StickyAI();
            else NormalAI();
        }

        private void UpdateAlpha()
        {
            // Slowly remove alpha as it is present
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= ALPHA_REDUCTION;
            }

            // If alpha gets lower than 0, set it to 0
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }

        private void NormalAI()
        {
            if (!frameSet)
            {
                Projectile.frame = Main.rand.Next(Main.projFrames[Type]);
                frameSet = true;
            }

            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() +
                (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(45f) : MathHelper.ToRadians(135f));
            Projectile.SetVisualOffsets(new Vector2(48, 54));
        }

        private void StickyAI()
        {
            // These 2 could probably be moved to the ModifyNPCHit hook, but in vanilla they are present in the AI
            Projectile.ignoreWater = true; // Make sure the projectile ignores water
            Projectile.tileCollide = false; // Make sure the projectile doesn't collide with tiles anymore

            int projTargetIndex = TargetWhoAmI;
            if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
            {
                Projectile.Center = Main.npc[projTargetIndex].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
            }
            else Projectile.Kill();
        }
    }
}
