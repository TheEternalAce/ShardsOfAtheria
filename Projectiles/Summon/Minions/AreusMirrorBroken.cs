using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class AreusMirrorBroken : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 40;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
            }
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
            for (int i = 0; i < 3; i++)
            {
                Vector2 velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi);
                velocity *= 12 * (1f - Main.rand.NextFloat(0.33f));
                var projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
                    ModContent.ProjectileType<AreusMirrorShard>(), Projectile.damage / 3, Projectile.knockBack, Main.myPlayer);
                projectile.originalDamage = Projectile.damage / 3;
            }
        }
    }
}