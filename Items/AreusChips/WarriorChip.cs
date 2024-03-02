using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class WarriorChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            DamageClass = DamageClass.Melee;
        }

        public override void UpdateChip(Player player)
        {
            base.UpdateChip(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.IronBroadsword)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.LeadBroadsword)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}