using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class HeatedPan : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 54;

            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.mana = 14;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;

            Item.shootSpeed = 16;
            Item.rare = ItemDefaults.RarityPreBoss;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
            Item.shoot = ModContent.ProjectileType<IronBall>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 16)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}