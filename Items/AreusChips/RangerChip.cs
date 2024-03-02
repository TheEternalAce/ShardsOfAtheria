using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class RangerChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            DamageClass = DamageClass.Ranged;
        }

        public override void UpdateChip(Player player)
        {
            base.UpdateChip(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.IronBow)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.LeadBow)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}