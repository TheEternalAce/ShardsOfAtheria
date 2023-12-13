using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusFlareGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 22;

            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0f;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 180000;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Flare;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 12)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.SoulofLight, 8)
                .AddIngredient(ItemID.CrystalShard, 14)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}