using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus.AreusSaber
{
    public abstract class AreusSlashes : ModProjectile
    {
        public int num1;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            SoAGlobalProjectile.AreusProj.Add(Type);
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 30;
        }

        public override void AI()
        {
            UpdateVisuals();

            Player player = Main.player[Projectile.owner];

            Projectile.Center = player.Center;

            Projectile.rotation = 0;
            if (++num1 >= 10)
            {
                Projectile.timeLeft--;
            }
            Projectile.netUpdate = true;
        }

        public void UpdateVisuals()
        {
            if (++Projectile.frameCounter == 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame == Main.projFrames[Type])
                {
                    Projectile.Kill();
                }
            }
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1);
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.velocity.X = 15f * player.direction;
                }
                Projectile.velocity.Y = 0;
                Projectile.ai[1] = 1;
            }
            return base.PreAI();
        }
    }

    public class AreusSlash1 : AreusSlashes
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 88;
            Projectile.height = 50;
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<AreusSlash2>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
            }

            base.Kill(timeLeft);
        }
    }

    public class AreusSlash2 : AreusSlashes
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 94;
            Projectile.height = 58;
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<AreusSlash3>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
            }
            base.Kill(timeLeft);
        }
    }

    public class AreusSlash3 : AreusSlashes
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 86;
            base.SetDefaults();
        }

        public override void AI()
        {
            base.AI();
            Player player = Main.player[Projectile.owner];

            if (Projectile.frame == 2 && Projectile.ai[0] == 0f)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 16;
                    if (player.direction == (Main.MouseWorld.X < player.Center.X ? 1 : -1))
                    {
                        velocity = -velocity;
                    }
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, velocity, ModContent.ProjectileType<AreusSlash4>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                    Projectile.ai[0] = 1f;
                }
            }
        }
    }
}