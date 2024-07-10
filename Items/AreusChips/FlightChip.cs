using ShardsOfAtheria.Items.BuffItems;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria.ID;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class FlightChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.maxStack = 1;
            slotType = SlotChest;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient<ChargedFlightPotion>(3)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}