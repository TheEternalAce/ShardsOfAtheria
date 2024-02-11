using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Static : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = ModContent.BuffType<ElectricShock>();
            dustType = DustID.Sand;
        }

        public override void AI()
        {
            base.AI();
            if (++Projectile.ai[0] >= 10)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SandTrail>(), Projectile.damage, 0);
                Projectile.ai[0] = 0;
            }
        }
    }
}