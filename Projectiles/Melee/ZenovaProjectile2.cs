using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class ZenovaProjectile2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 10;

            Projectile.AddElement(0);
            Projectile.AddElement(1);
            Projectile.AddElement(2);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(14);
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
            Projectile.timeLeft = 180;

            DrawOffsetX = -62;
            DrawOriginOffsetX = 31;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(Main.projFrames[Type]);
            Projectile.originalDamage = Projectile.damage;
            Projectile.damage = 0;
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.frame == 7)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ProjectileID.DaybreakExplosion, Projectile.damage, Projectile.knockBack, Projectile.owner);
                return;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center); // Play a death sound
            Vector2 usePos = Projectile.position; // Position to use for dusts

            // Please note the usage of MathHelper, please use this!
            // We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2(); // rotation vector to use for dust velocity
            usePos += rotVector * 16f;

            // Declaring a constant in-line is fine as it will be optimized by the compiler
            // It is however recommended to define it outside method scope if used elswhere as well
            // They are useful to make numbers that don't change more descriptive
            const int NUM_DUSTS = 20;

            // Spawn some dusts upon javelin death
            for (int i = 0; i < NUM_DUSTS; i++)
            {
                // Create a new dust
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Tin);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff<ZenovaJavelin>())
            {
                int javelinCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == ModContent.ProjectileType<ZenovaProjectile>() && p.ai[0] == 1f && p.ai[1] == target.whoAmI)
                    {
                        javelinCount++;
                    }
                }
                modifiers.FlatBonusDamage += javelinCount * 3;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int buffType = ModContent.BuffType<ZenovaJavelin>();
            switch (Projectile.frame)
            {
                default:
                    break;
                case 1:
                    buffType = BuffID.Ichor;
                    break;
                case 2:
                    buffType = BuffID.Venom;
                    break;
                case 3:
                    buffType = ModContent.BuffType<ElectricShock>();
                    break;
                case 5:
                    buffType = BuffID.OnFire3;
                    break;
                case 7:
                    buffType = BuffID.Daybreak;
                    break;
                case 8:
                    buffType = 169;
                    break;
                case 9:
                    buffType = ModContent.BuffType<ElectricShock>();
                    Projectile.Kill();
                    break;
            }
            target.AddBuff(buffType, 900);
        }

        private int spinTime = 40;
        private int gravityTime = 24;
        private const int ALPHA_REDUCTION = 25;

        public override void AI()
        {
            UpdateAlpha();
            Projectile.SetVisualOffsets(new Vector2(80, 84), true);
            if (--spinTime > 0)
            {
                SpinAI();
            }
            else
            {
                TrackCursor();
            }
        }

        private void UpdateAlpha()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= ALPHA_REDUCTION;
            }

            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }

        private void TrackCursor()
        {
            if (Projectile.damage == 0)
            {
                Projectile.damage = Projectile.originalDamage;
            }
            var player = Projectile.GetPlayerOwner();
            if (player.IsLocal())
            {
                Projectile.Track(Main.MouseWorld);
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }

        private void SpinAI()
        {
            Projectile.ApplyGravity(ref gravityTime);
            Projectile.rotation += MathHelper.ToRadians(15f);
        }
    }
}
