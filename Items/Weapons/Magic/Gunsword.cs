using ShardsOfAtheria.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class Gunsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 4.5f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<GunswordProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBroadsword)
                .AddIngredient(ItemID.Revolver)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.GoldBroadsword)
                .AddIngredient(ItemID.Revolver)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}