using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Poison : HarpyFeathers
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Poisoned;
            dustType = DustID.Poisoned;
        }
    }
}