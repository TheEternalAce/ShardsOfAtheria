using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class NinjaChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            DamageClass = DamageClass.Throwing;
        }

        public override void UpdateChip(Player player)
        {
            base.UpdateChip(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.Shuriken, 100)
                .AddIngredient(ItemID.Wire, 20)
                .AddCondition(SoAConditions.ThrowingWeapon)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}