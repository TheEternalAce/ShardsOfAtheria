using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic.Gambit;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Magic.EntropyCatalyst
{
    public class CatalystBomb : ModProjectile
    {
        bool Boosted => Projectile.ai[0] > 0;
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(3, 4);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(14);
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;

            DrawOriginOffsetY = -7;
        }

        int shootDelay = 10;
        public override void AI()
        {
            if (shootDelay > 0) shootDelay--;

            if (Boosted)
            {
                var dust = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Vector2.Zero);
                dust.noGravity = true;
                dust.fadeIn = 1;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                Projectile.netUpdate = true;
            }
            else if (shootDelay <= 0)
            {
                Projectile.ai[1]++;
                if (Projectile.ai[1] > 6) Projectile.ApplyGravity();
                if (Projectile.ai[1] > 15) Projectile.velocity.X *= 0.99f;
                Projectile.rotation += Projectile.velocity.X * 0.05f;
                foreach (var proj in Main.ActiveProjectiles)
                {
                    if (proj.friendly &&
                        proj.whoAmI != Projectile.whoAmI &&
                        proj.owner == Projectile.owner &&
                        (proj.aiStyle == ProjAIStyleID.Arrow || proj.aiStyle == 0) &&
                        proj.damage > 0)
                    {
                        if (proj.Distance(Projectile.Center) <= 15)
                        {
                            if (proj.type == Type && proj.ai[0] == 0) break;
                            proj.Kill();
                            Projectile.Kill();
                            Explode();
                            break;
                        }
                    }
                }
            }
        }

        public bool ValidCoin(Projectile projectile)
        {
            if (!projectile.active) return false;
            if (projectile.type != ModContent.ProjectileType<ElecCoin>()) return false;
            if (projectile.whoAmI == Projectile.whoAmI) return false;
            if (projectile.owner != Projectile.owner) return false;
            return true;
        }

        // Make sure the bomb doesn't explode when hitting coins
        public override void OnKill(int timeLeft)
        {
            if (timeLeft == 0) Explode();
        }

        private void Explode()
        {
            if (Projectile.GetPlayerOwner().IsLocal())
            {
                float multiplier = Boosted ? 1.5f : 0.75f;
                int damage = (int)(Projectile.damage * multiplier);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                    ModContent.ProjectileType<FieryExplosion>(), damage, Projectile.knockBack);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return base.OnTileCollide(oldVelocity);
        }
    }
}
