using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Summon.Minions.Sentry;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class AreusCrate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5);
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;

            Item.damage = 62;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 3f;

            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 10f;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.shoot = ModContent.ProjectileType<AreusCrateProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(20)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.BeetleHusk, 8)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}