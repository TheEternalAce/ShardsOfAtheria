using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles.Needles;

namespace SagesMania.Items.Weapons.Ranged.Needles
{
    public class CopperNeedle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Lower defense briefly");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.White;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<CopperNeedleProjectile>();
            item.shootSpeed = 20;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.consumable = true;
            item.damage = 5;
            item.ranged = true;
            item.UseSound = SoundID.Item1;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperOre);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}