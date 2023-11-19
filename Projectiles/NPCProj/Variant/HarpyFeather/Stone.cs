using Terraria;
using Terraria.ID;

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
    }
}