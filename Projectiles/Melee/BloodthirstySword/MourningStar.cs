using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.BloodthirstySword
{
    public class MourningStar : CoolSword
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 70;
            ProjectileID.Sets.TrailingMode[Type] = -1;
            Projectile.AddAreus(true);
            Projectile.AddDamageType(6);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 50;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            hitsLeft = 5;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (player.Distance(target.Center) < 130)
            {
                freezeFrame = 6;
                target.immune[Projectile.owner] = 2;
                var copyHit = hit;
                copyHit.Damage /= 2;
                target.StrikeNPC(copyHit);
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0)
            {
                swingDirection *= -1;
            }
            if (swingDirection == -Projectile.direction)
            {
                swordReach = 100;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            base.AI();
            if (Main.player[Projectile.owner].itemAnimation <= 1)
            {
                Main.player[Projectile.owner].Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.5f) - MathHelper.PiOver2 * 1.5f) * -swingDirection * 1.1f);
        }

        public override void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
            if (progress > 0.85f)
            {
                Projectile.Opacity = 1f - (progress - 0.85f) / 0.15f;
            }

            Projectile.oldPos[0] = AngleVector * 60f * Projectile.scale;
            Projectile.oldRot[0] = Projectile.oldPos[0].ToRotation() + MathHelper.PiOver4;

            // Manually updating oldPos and oldRot 
            for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
            {
                Projectile.oldPos[i] = Projectile.oldPos[i - 1];
                Projectile.oldRot[i] = Projectile.oldRot[i - 1];
            }

            if (progress == 0.5f && Main.myPlayer == Projectile.owner)
            {
                if (swingDirection == Projectile.direction)
                {
                    var spawnpos = Main.MouseWorld + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 130f;
                    var vector = Vector2.Normalize(Main.MouseWorld - spawnpos) * 32;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnpos, vector,
                        ModContent.ProjectileType<BloodCutter>(), Projectile.damage / 2, 0f, Projectile.owner);
                }
            }
        }

        public override float SwingProgress(float progress)
        {
            return SwingProgressAequus(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            if (progress > 0.8f)
            {
                float p = 1f - (1f - progress) / 0.2f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            if (progress < 0.35f)
            {
                float p = 1f - progress / 0.35f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawSwish();
            if (swingDirection == -Projectile.direction)
            {
                string path = "ShardsOfAtheria/Projectiles/Melee/BloodthirstySword/MourningStarBloodEdge";
                return SingleEdgeSwordDraw(lightColor, path);
            }
            return SingleEdgeSwordDraw<TheMourningStar>(lightColor);
        }
    }
}