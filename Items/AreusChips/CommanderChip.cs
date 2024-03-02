using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class CommanderChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            DamageClass = DamageClass.Summon;
        }

        public override void UpdateChip(Player player)
        {
            base.UpdateChip(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.FallenStar, 5)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}