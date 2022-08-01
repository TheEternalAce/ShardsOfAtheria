using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class FragmentEntropy : SlayerItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Entropic Fragment");
            Tooltip.SetDefault("'Feelings of universal decay emanate from this fragment'");
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 18);
            Item.rare = ItemRarityID.Red;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentNebula)
                .AddIngredient(ItemID.FragmentSolar)
                .AddIngredient(ItemID.FragmentStardust)
                .AddIngredient(ItemID.FragmentVortex)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
