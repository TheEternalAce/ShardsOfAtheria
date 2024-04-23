using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    [AutoloadEquip(EquipType.Body)]
    public class GhostFairyShirt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityDevSet;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AcesJacket>()
                .AddCondition(Condition.InGraveyard)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddCondition(Condition.Halloween)
                .Register();
        }
    }
}
