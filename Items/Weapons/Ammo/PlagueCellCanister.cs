using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class PlagueCellCanister : ModItem
    {
        public override string Texture => ModLoader.GetMod("Calamity").Find<ModItem>("PlagueCellCanister").Texture;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("Calamity", out var _);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            Item.AddDamageType(0, 6, 8);
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 34;
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = Item.sellPrice(0, 1);
            Item.ammo = ModContent.ItemType<PlagueCell>();
        }

        public override void AddRecipes()
        {
            var modItem = ModLoader.GetMod("Calamity").Find<ModItem>("PlagueCellCanister");
            CreateRecipe()
                .AddIngredient(modItem.Type)
                .Register();
            Recipe.Create(modItem.Type)
                .AddIngredient(Type)
                .Register();
        }
    }
}