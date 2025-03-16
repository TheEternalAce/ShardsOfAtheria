using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Melee.AreusSwordProjs
{
    public class AreusSwordProj : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(5);
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 100;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 5;
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

        public override void OnKill(int timeLeft)
        {
            var player = Main.player[Projectile.owner];
            if (player.IsLocal())
            {
                int boxSize = 100;
                Rectangle spawnRegion = new(
                    (int)player.Center.X - boxSize / 2,
                    (int)player.Center.Y - boxSize / 2,
                    boxSize, boxSize
                );
                var boxOffset = player.Center - Main.MouseWorld;
                boxOffset.Normalize();
                boxOffset *= boxSize;
                spawnRegion.X += (int)boxOffset.X;
                spawnRegion.Y += (int)boxOffset.Y;
                for (int i = 0; i < 5; i++)
                {
                    var position = Main.rand.NextVector2FromRectangle(spawnRegion);
                    var vector = Main.MouseWorld - position;
                    vector.Normalize();
                    vector *= 16;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, vector,
                        ModContent.ProjectileType<ElectricBlade>(), Projectile.damage,
                        Projectile.knockBack, player.whoAmI);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawSwish();
            return SingleEdgeSwordDraw<AreusSword>(lightColor);
        }
    }
}