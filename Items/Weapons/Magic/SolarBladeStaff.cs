using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class SolarBladeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(3);
            Item.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;

            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 5;
            Item.mana = 12;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 16;
            Item.rare = ItemDefaults.RarityJungle;
            Item.value = ItemDefaults.ValueDungeon;
            Item.shoot = ModContent.ProjectileType<SolarBlade>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.JungleRose)
                .AddIngredient(ItemID.JungleSpores, 15)
                .AddIngredient(ItemID.Vine, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}