using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    [AutoloadEquip(EquipType.Legs)]
    public class GhostFairyPants : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityDevSet;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AcesPants>()
                .AddCondition(Condition.InGraveyard)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddCondition(Condition.Halloween)
                .Register();
        }
    }
}
