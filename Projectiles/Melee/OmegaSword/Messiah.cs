using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Melee.OmegaSword
{
    public class Messiah : ModProjectile
    {
        public const int FadeInDuration = 7;
        public const int FadeOutDuration = 4;

        public const int TotalDuration = 18;

        // The "width" of the blade
        public float CollisionWidth => 10f * Projectile.scale;

        bool shouldMove = true;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void SetStaticDefaults()
        {
            Projectile.MakeTrueMelee();
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(18); // This sets width and height to the same value (important when projectiles can rotate)
            Projectile.aiStyle = -1; // Use our own AI to customize how it behaves, if you don't want that, keep this at ProjAIStyleID.ShortSword. You would still need to use the code in SetVisualOffsets() though
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true; // Prevents hits through tiles. Most melee weapons that use projectiles have this
            Projectile.extraUpdates = 1; // Update 1+extraUpdates times per tick
            Projectile.timeLeft = 360; // This value does not matter since we manually kill it earlier, it just has to be higher than the duration we use in AI
            Projectile.hide = true; // Important when used alongside player.heldProj. "Hidden" projectiles have special draw conditions
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Timer += 1;
            if (Timer >= TotalDuration)
            {
                // Kill the projectile if it reaches it's intented lifetime
                Projectile.Kill();
                return;
            }
            else
            {
                // Important so that the sprite draws "in" the player's hand and not fully infront or behind the player
                player.heldProj = Projectile.whoAmI;
                player.itemAnimation = 10;
                player.itemTime = 10;
            }

            // Fade in and out
            // GetLerpValue returns a value between 0f and 1f - if clamped is true - representing how far Timer got along the "distance" defined by the first two parameters
            // The first call handles the fade in, the second one the fade out.
            // Notice the second call's parameters are swapped, this means the result will be reverted
            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

            // Keep locked onto the player, but extend further based on the given velocity (Requires ShouldUpdatePosition returning false to work)
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            if (shouldMove)
            {
                Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);
            }

            // Set spriteDirection based on moving left or right. Left -1, right 1
            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

            // Point towards where it is moving, applied offset for top right of the sprite respecting spriteDirection
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

            // The code in this method is important to align the sprite with the hitbox how we want it to
            SetVisualOffsets();
        }

        private void SetVisualOffsets()
        {
            // 32 is the sprite size (here both width and height equal)
            const int HalfSpriteWidth = 56 / 2;
            const int HalfSpriteHeight = 56 / 2;

            int HalfProjWidth = Projectile.width / 2;
            int HalfProjHeight = Projectile.height / 2;

            // Vanilla ModContent.GetInstanceConfiguration for "hitbox in middle of sprite"
            DrawOriginOffsetX = 0;
            DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
            DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);

            // Vanilla ModContent.GetInstanceConfiguration for "hitbox towards the end"
            //if (Projectile.spriteDirection == 1)
            //{
            //    DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth);
            //    DrawOffsetX = (int)-DrawOriginOffsetX * 2;
            //    DrawOriginOffsetY = 0;
            //}
            //else
            //{
            //    DrawOriginOffsetX = HalfProjWidth - HalfSpriteWidth;
            //    DrawOffsetX = 0;
            //    DrawOriginOffsetY = 0;
            //}
        }

        public override bool ShouldUpdatePosition()
        {
            // Update Projectile.Center manually
            return false;
        }

        public override void CutTiles()
        {
            // "cutting tiles" refers to breaking pots, grass, queen bee larva, etc.
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
            Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            // "Hit anything between the player and the tip of the sword"
            // shootSpeed is 2.1f for reference, so this is basically plotting 12 pixels ahead from the center
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity * 12;
            float collisionPoint = 0f; // Don't need that variable, but required as parameter
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (!player.IsLocal()) return;
            SoundEngine.PlaySound(SoA.Rekkoha.WithVolumeScale(0.5f), Projectile.Center);
            int spacing = 150;
            int numFire = 9;
            int offset = -spacing * (numFire / 2);
            for (int i = 0; i < numFire; i++)
            {
                var position = player.Center - new Vector2(offset, 600f);
                position.Y -= Math.Abs(offset);
                Vector2 velocity = new(0, 10);

                Projectile fire = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(),
                    position, velocity, ProjectileID.ApprenticeStaffT3Shot, Projectile.damage,
                    Projectile.knockBack, player.whoAmI);
                fire.tileCollide = false;
                fire.DamageType = DamageClass.Melee;
                fire.timeLeft = 120;
                offset += spacing;
            }
            player.velocity *= 0f;
            player.AddBuff(ModContent.BuffType<MessiahRekkoha>(), 60);
        }
    }
}
