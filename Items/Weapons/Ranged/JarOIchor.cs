using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;

namespace SagesMania.Items.Weapons.Ranged
{
    public class JarOIchor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jar O' Ichor");
            Tooltip.SetDefault("Throws a jar of ichor which lowers defense");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(silver: 35);
            item.rare = ItemRarityID.Red;
            item.autoReuse = false;
            item.shoot = ModContent.ProjectileType<JarOIchorProjectile>();
            item.shootSpeed = 15;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.consumable = true;
            item.damage = 21;
            item.ranged = true;
            item.UseSound = SoundID.Item106;
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddIngredient(ItemID.Ichor, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}