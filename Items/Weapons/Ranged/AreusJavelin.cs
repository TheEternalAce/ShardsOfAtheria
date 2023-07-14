using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusJavelin : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 54;

            Item.damage = 115;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7f;
            Item.crit = 10;

            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 250000;
            Item.shoot = ModContent.ProjectileType<AreusJavelinThrown>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddRecipeGroup(ShardsRecipes.Gold, 5)
                .AddIngredient(ItemID.LunarBar, 16)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
