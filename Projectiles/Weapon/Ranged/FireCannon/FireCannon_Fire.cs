using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.FireCannon
{
    public class FireCannon_Fire1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.GetPlayerOwner().Shards().Overdrive)
            {
                Projectile.CallStorm(3);
                return base.OnTileCollide(oldVelocity);
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                Vector2.Zero, ModContent.ProjectileType<ElecFirePillar>(),
                Projectile.damage / 3, 0, Projectile.owner);
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Torch);
            }
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Electric);
            }
            if (Projectile.GetPlayerOwner().Shards().Overdrive)
            {
                Projectile.CallStorm(3);
            }
        }
    }
    public class FireCannon_Fire2 : FireCannon_Fire1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = 5;
        }

        int timer = 0;
        int TimerMax = 1;
        public override void AI()
        {
            base.AI();
            if (++timer >= TimerMax)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                    ModContent.ProjectileType<ElectricTrailFriendly>(), Projectile.damage / 2, 0, Projectile.owner);
                timer = 0;
            }
        }
    }
    public class FireCannon_Fire3 : FireCannon_Fire1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft *= 2;
            Projectile.penetrate = 10;
        }

        int timer = 0;
        int TimerMax = 18;
        public override void AI()
        {
            base.AI();
            if (++timer >= TimerMax)
            {
                int numProjectiles = 6;
                for (int i = 0; i < numProjectiles; i++)
                {
                    float angle = 360 / numProjectiles;
                    float rotAngle = MathHelper.ToRadians(angle * i);
                    rotAngle += MathHelper.ToRadians(angle / 2);
                    Vector2 vector = Projectile.velocity.RotatedBy(rotAngle);
                    vector.Normalize();
                    vector *= 14;
                    Projectile spark = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(),
                        Projectile.Center, vector, ProjectileID.ThunderSpearShot, Projectile.damage / 2, 0,
                        Projectile.owner);
                    spark.DamageType = Projectile.DamageType;
                }
                timer = 0;
            }
        }
    }
}
