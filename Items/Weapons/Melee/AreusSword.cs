using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusSwordProjs;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.UpgradeableItem.Add(Type);
            SoAGlobalItem.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 76;

            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<AreusSwordProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddRecipeGroup(ShardsRecipes.Gold, 6)
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }
    }
}