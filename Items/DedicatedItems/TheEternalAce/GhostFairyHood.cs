using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    [AutoloadEquip(EquipType.Head)]
    public class GhostFairyHood : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityDevSet;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AcesGoldFoxMask>()
                .AddCondition(Condition.InGraveyard)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddCondition(Condition.Halloween)
                .Register();
        }
    }
}
