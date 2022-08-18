using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Potions;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class ElectricBolt : ModProjectile {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.arrow = true;
            Projectile.light = 1;
            Projectile.timeLeft = 600;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X* .2f, Projectile.velocity.Y* .2f, 200, Scale: 1f);
            }
        }
    }
}
