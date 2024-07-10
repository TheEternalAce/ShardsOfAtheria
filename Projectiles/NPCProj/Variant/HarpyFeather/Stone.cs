using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Stone : HarpyFeathers
    {
        public override bool DebuffCondition => base.DebuffCondition && Main.rand.NextBool(10);

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Stoned;
            dustType = DustID.Stone;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            base.OnHitPlayer(target, info);
            if (SoA.Massochist()) Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ElectricExplosion_Hostile>(), Projectile.damage, 0);
        }
    }
}