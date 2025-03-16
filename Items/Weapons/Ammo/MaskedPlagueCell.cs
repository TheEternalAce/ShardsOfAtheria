using ShardsOfAtheria.Projectiles.Ranged.PlagueRail;
using ShardsOfAtheria.Utilities;
using Terraria;
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
            Item.AddDamageType(0, 6, 8);
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.rare = ItemDefaults.RarityMechs;
            Item.value = Item.sellPrice(silver: 2);
            Item.ammo = ModContent.ItemType<PlagueCell>();
            Item.shoot = ModContent.ProjectileType<PlagueBeam2>();
        }

        public override void AddRecipes()
        {
            var modItem = ModLoader.GetMod("GMR").Find<ModItem>("MaskedPlagueModule");
            CreateRecipe(100)
                .AddIngredient(modItem.Type)
                .Register();
            Recipe.Create(modItem.Type)
                .AddIngredient(Type, 100)
                .Register();
        }
    }
}