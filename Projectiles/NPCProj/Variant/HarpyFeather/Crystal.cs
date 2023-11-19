using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Crystal : HarpyFeathers
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Confused;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkCrystalShard, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default, 0.75f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default, 0.75f);
            }
        }
    }
}