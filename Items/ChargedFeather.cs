using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items
{
    public class ChargedFeather : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Electricity flows between its barbs'");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
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