using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Melee.AreusOrbit
{
    public class OrbiterMagnet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // The following sets are only applicable to yoyo that use aiStyle 99.
            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 350;
            // YoyosTopSpeed is top speed of the yoyo Projectile. 
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 16.8f;

            Projectile.AddElementElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            DrawOffsetX = -10;
        }

        int timer;
        public override void AI()
        {
            base.AI();
            var player = Projectile.GetPlayerOwner();
            int type = ModContent.ProjectileType<AreusOrbitingSaw>();
            int sawCount = player.ownedProjectileCounts[type];
            if (sawCount < 5)
            {
                if (player.IsLocal())
                {
                    if (Main.mouseRight)
                    {
                        if (--timer <= 0)
                        {
                            timer = 12;
                            var vector = Main.MouseWorld - player.Center;
                            vector.Normalize();
                            vector *= 16;
                            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, vector, type,
                                Projectile.damage, Projectile.knockBack, Main.myPlayer);
                        }
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }
    }
}
