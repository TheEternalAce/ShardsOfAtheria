using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class AreusArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            Item.AddAreus();
            Item.AddDamageType(7);
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
            Item.shoot = ModContent.ProjectileType<AreusArrowProj>();
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.WoodenArrow, 100)
                .AddIngredient<AreusShard>()
                .AddIngredient(ItemID.GoldBar)
                .AddIngredient<Jade>()
                .AddIngredient(ItemID.SoulofFlight)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}