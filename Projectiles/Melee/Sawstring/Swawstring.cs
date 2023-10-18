using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.Sawstring
{
    public class Swawstring : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // The following sets are only applicable to yoyo that use aiStyle 99.
            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300;
            // YoyosTopSpeed is top speed of the yoyo Projectile. 
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15f;

            Projectile.AddElementElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void PostAI()
        {
            var player = Projectile.GetPlayerOwner();
            if (Projectile.Distance(player.Center) > 80)
            {
                if (Projectile.ai[0] < 0)
                {
                    Projectile.Kill();
                    var vector = Projectile.Center - player.Center;
                    vector.Normalize();
                    vector *= 15;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                        vector, ModContent.ProjectileType<AreusSaw>(), (int)(Projectile.damage * 0.8f),
                        0, Projectile.owner);
                }
            }
        }
    }
}
