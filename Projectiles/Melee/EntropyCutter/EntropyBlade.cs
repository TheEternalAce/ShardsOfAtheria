using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.EntropyCutter
{
    public class EntropyBlade : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 230;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 5;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0)
            {
                swingDirection *= -1;
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
            if (progress == 0.5f && Main.myPlayer == Projectile.owner)
            {
                var spawnpos = Main.MouseWorld + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 130f;
                var vector = Vector2.Normalize(Main.MouseWorld - spawnpos) * 32;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnpos, vector,
                    ModContent.ProjectileType<EntropySlash>(), Projectile.damage / 3, 0f, Projectile.owner);

                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(15);
                var player = Main.player[Projectile.owner];
                var position = player.Center;
                var velocity = AngleVector * 16f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, perturbedSpeed,
                        ModContent.ProjectileType<EntropySickle>(), Projectile.damage / 6, Projectile.knockBack,
                        player.whoAmI);
                }
            }
            base.UpdateSwing(progress, interpolatedSwingProgress);
        }

        public override float SwingProgress(float progress)
        {
            return GenericSwing2(progress);
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
            return SingleEdgeSwordDraw(lightColor);
        }
    }
}