using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ByteCrush
{
    public class ByteBlock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElementElec();
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;

            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
        }

        Vector2 InitialVelocity;
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.originalDamage = Projectile.damage;
            Projectile.damage = 0;
            InitialVelocity = Projectile.velocity;
            Projectile.velocity = Vector2.Zero;
        }

        int crushDelay = 5;
        public override void AI()
        {
            if (crushDelay > 0)
            {
                crushDelay--;
            }
            if (crushDelay <= 0)
            {
                Projectile.velocity = InitialVelocity;
            }

            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active)
                {
                    if (projectile.type == Type)
                    {
                        if (projectile.whoAmI != Projectile.whoAmI)
                        {
                            if (projectile.owner == Projectile.owner)
                            {
                                if (Projectile.Hitbox.Intersects(projectile.Hitbox))
                                {
                                    projectile.Kill();
                                    Projectile.Kill();
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            var position = Projectile.Center;
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 16; i++)
            {
                var velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(10f, 16f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, velocity,
                    ModContent.ProjectileType<BitBlock>(), Projectile.originalDamage,
                    Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
