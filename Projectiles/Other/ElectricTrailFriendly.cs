using ShardsOfAtheria.Buffs.AnyDebuff;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class ElectricTrailFriendly : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Blank";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<ElectricTrail>());
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric,
                       Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
                dust.noGravity = true;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }
    }
}