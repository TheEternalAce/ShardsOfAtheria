using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class ChargedFeather : ModItem
    {
        public override void SetStaticDefaults()
        {
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;

            SacrificeTotal = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 40;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Green;
            Item.value = 1000;
        }
    }
}