using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class PuncherBlast : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.extraUpdates = 5;
            Projectile.timeLeft = 15;
            Projectile.penetrate = 2;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 5;
        }

        public override void OnSpawn(IEntitySource source)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(15);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = Projectile.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                perturbedSpeed.Normalize();
                for (int j = 0; j < 10; j++)
                {
                    var dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond);
                    dust.noGravity = true;
                    dust.velocity = perturbedSpeed * (4f + 0.1f * j);
                }
            }
        }

        public override void AI()
        {
            var dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond);
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.8f);
        }
    }
}
