using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Void : HarpyFeathers
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.OnFire;
            dustType = DustID.Torch;
        }
    }
}