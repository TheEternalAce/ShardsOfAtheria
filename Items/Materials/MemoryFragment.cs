using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class MemoryFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 44;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1);
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 1f * Main.essScale);
        }
    }
}
