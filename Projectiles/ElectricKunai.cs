using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Projectiles
{
    public class ElectricKunai : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.scale = .5f;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.arrow = false;
            Projectile.light = 1;
            Projectile.extraUpdates = 1;

            DrawOffsetX = -28;
            DrawOriginOffsetX = 14;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 10*60);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 20f)
            {
                Projectile.ai[0] = 15f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            }
        }
    }
}
