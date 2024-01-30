using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon
{
    public class ElectricSpark : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementElec();
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 60;
            Projectile.light = 0.5f;
            Projectile.penetrate = 3;
        }

        public override void AI()
        {
            // Tweaked Wand of Sparking spark ai
            Projectile.ai[0]++;
            int num140 = 100;
            if (Projectile.ai[0] > 20f)
            {
                int num151 = 40;
                float num163 = Projectile.ai[0] - 20f;
                num140 = (int)(100f * (1f - num163 / num151));
                if (num163 >= num151)
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[0] <= 10f)
            {
                num140 = (int)Projectile.ai[0] * 10;
            }
            if (Main.rand.Next(100) < num140)
            {
                var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 150);
                dust1.position = (dust1.position + Projectile.Center) / 2f;
                dust1.noGravity = true;
                var dust2 = dust1;
                var dust3 = dust2;
                dust3.velocity *= 1.5f;
                dust2 = dust1;
                dust3 = dust2;
                dust3.scale *= 0.75f;
                dust2 = dust1;
                dust3 = dust2;
                dust3.velocity += Projectile.velocity;
            }
            if (Projectile.ai[0] >= 20f)
            {
                Projectile.velocity.X *= 0.99f;
                Projectile.velocity.Y += 0.1f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<ElectricShock>(600);
        }
    }
}
