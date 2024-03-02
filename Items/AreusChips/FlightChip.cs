using ShardsOfAtheria.Items.BuffItems;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class FlightChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            slotType = SlotChest;
        }

        public override void UpdateChip(Player player)
        {
            player.wingTimeMax += 20;
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