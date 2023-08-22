using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusStrikeChain : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;

            Item.damage = 160;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 26;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 200000;
            Item.shoot = ModContent.ProjectileType<StrikeChain>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 17)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient<Jade>(2)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] == 0;
        }
    }
}