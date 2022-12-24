using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Globals.Elements;
using ShardsOfAtheria.Items.Potions;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class HardlightBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.MetalProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
        }
    }
}
