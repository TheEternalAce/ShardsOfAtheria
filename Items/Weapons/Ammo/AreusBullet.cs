using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class AreusBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;

            Item.rare = ItemDefaults.RarityAreus;
            Item.value = ItemDefaults.ValueDungeon;
            Item.shoot = ModContent.ProjectileType<AreusBulletProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.MusketBall, 100)
                .AddIngredient(ModContent.ItemType<AreusShard>())
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}