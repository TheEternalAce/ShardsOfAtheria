using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class SpeedChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            slotType = SlotLegs;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
            player.moveSpeed += 0.2f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.SwiftnessPotion, 3)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}