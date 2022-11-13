using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class LightningBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 200;

            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.alpha -= 25;
            if (Projectile.alpha <= 0)
            {
                Projectile.alpha = 0;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(270);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
        }
    }
}
