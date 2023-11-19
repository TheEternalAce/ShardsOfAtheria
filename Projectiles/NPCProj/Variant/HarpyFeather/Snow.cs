using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Snow : HarpyFeathers
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Frostburn;
            dustType = DustID.Snow;
        }
    }
}