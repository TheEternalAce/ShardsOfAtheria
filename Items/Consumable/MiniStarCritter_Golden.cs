using ShardsOfAtheria.NPCs;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public class MiniStarCritter_Golden : MiniStarCritter
    {
        public override string Texture => ModContent.GetInstance<MiniAreusStar_Golden>().Texture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = ModContent.NPCType<MiniAreusStar_Golden>();
        }
    }
}
