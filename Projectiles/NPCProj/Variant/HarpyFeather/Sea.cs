using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Sea : HarpyFeathers
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Venom;
            dustType = DustID.Water;
        }
    }
}