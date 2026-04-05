using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Sinner;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class PrideGold : ModProjectile
    {
        int gravityTimer = 0;

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.GoldCoin;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(1);

            SoAGlobalProjectile.Metalic.Add(Type, 0.5f);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 10; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        }

        public override void AI()
        {
            Projectile.SetVisualOffsets(new Vector2(6, 16), true);
            Projectile.rotation += MathHelper.ToRadians(32) * Projectile.direction;
            if (Main.rand.NextBool(3))
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GoldCoin, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }

            if (++Projectile.ai[0] >= 16)
            {
                Projectile.ApplyGravity(0.2f);
                gravityTimer = 16;
                Projectile.friendly = true;

                foreach (var proj in Main.ActiveProjectiles)
                {
                    bool canHitCoin = proj.aiStyle == ProjAIStyleID.Arrow || proj.aiStyle == 0 || proj.GetGlobalProjectile<SoAGlobalProjectile>().canHitCoin;

                    if (proj.friendly &&
                        proj.owner == Projectile.owner &&
                        canHitCoin &&
                        proj.damage > 0)
                    {
                        if (proj.Distance(Projectile.Center) <= 20 && proj.ModProjectile is not HitscanBullet && proj.ModProjectile is not BasicBeam)
                        {
                            HitCoin();
                            if (proj.ModProjectile is HitscanBullet bullet)
                                bullet.Stop();
                            else proj.Kill();
                            break;
                        }
                    }
                }
            }
        }

        public bool ValidTarget(Projectile projectile, int targetType)
        {
            if (!projectile.active) return false;
            if (projectile.type != targetType) return false;
            if (projectile.whoAmI == Projectile.whoAmI) return false;
            if (projectile.owner != Projectile.owner) return false;
            return true;
        }

        public void HitCoin()
        {
            var player = Projectile.GetPlayerOwner();
            var sinner = player.Sinner();
            player.AddBuff<EgoBoost>(300);
            sinner.PrideCancelAttack();
            sinner.PrideCancelAttack();
            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GoldCoin, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }
    }
}
