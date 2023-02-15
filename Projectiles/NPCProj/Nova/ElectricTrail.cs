using MMZeroElements;
using ShardsOfAtheria.Buffs.AnyDebuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class ElectricTrail : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Blank";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            dust.noGravity = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 300);
        }
    }
}