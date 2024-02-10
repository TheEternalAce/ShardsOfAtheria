using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class MaskedPlagueCell : ModItem
    {
        public override string Texture => ModLoader.GetMod("GMR").Find<ModItem>("MaskedPlagueModule").Texture;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("GMR", out var _);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 30;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 45);
            Item.ammo = ModContent.ItemType<PlagueCell>();
        }

        public override void AddRecipes()
        {
            var modItem = ModLoader.GetMod("GMR").Find<ModItem>("MaskedPlagueModule");
            CreateRecipe()
                .AddIngredient(modItem.Type)
                .Register();
            Recipe.Create(modItem.Type)
                .AddIngredient(Type)
                .Register();
        }
    }
}