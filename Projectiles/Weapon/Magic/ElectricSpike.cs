using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Potions;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class ElectricSpike : ModProjectile
    {
        public int flightTimer;
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.arrow = true;
            Projectile.light = 1;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;

            DrawOffsetX = 4;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            flightTimer++;
            if (flightTimer == 20)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.friendly = true;
                    Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 16f;
                }
            }
            if (flightTimer == 50)
                Projectile.tileCollide = true;
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X* .2f, Projectile.velocity.Y* .2f, 200, Scale: 1f);
            }
        }
    }
}
