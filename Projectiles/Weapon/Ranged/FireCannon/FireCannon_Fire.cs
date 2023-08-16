using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic.ElecGauntlet;
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

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.DamageType == DamageClass.Magic)
            {
                var player = Main.player[Projectile.owner];
                var gPlayer = player.GetModPlayer<ElecGauntletPlayer>();
                int type = ModContent.ProjectileType<ElecCannon>();

                if (gPlayer.UsedProjs.Contains(type))
                {
                    gPlayer.ComboDamage(type, ref modifiers);
                }
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}
