using ShardsOfAtheria.Globals;
using MMZeroElements;
using ShardsOfAtheria.Items.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.Messiah
{
    public class MessiahAirSlash : ModProjectile
    {
        public int num1;

        public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Melee/Messiah/MessiahRanbu2";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<MessiahRanbu2>());
            ProjectileElements.Fire.Add(Type);
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void AI()
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

            Player player = Main.player[Projectile.owner];
            (player.HeldItem.ModItem as TheMessiah).charge = 0;
            Projectile.Center = player.Center;

            Projectile.rotation = 0;
            if (++num1 >= 10)
            {
                Projectile.timeLeft--;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }

        public override void PostAI()
        {
            if (Projectile.ai[1] == 0)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.velocity.X = 15f * (Main.MouseWorld.X > Main.player[Projectile.owner].Center.X ? 1 : -1);
                }
                Projectile.velocity.Y = 0;
                Projectile.ai[1] = 1;
            }
            base.PostAI();
        }
    }
}