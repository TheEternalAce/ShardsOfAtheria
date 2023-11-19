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
            Item.ResearchUnlockCount = 1;
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;

            Item.damage = 33;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 3f;

            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 10f;
            Item.value = 125000;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<AreusCrateProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}