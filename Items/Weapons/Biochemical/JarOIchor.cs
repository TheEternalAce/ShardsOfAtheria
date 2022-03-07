using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon;

namespace ShardsOfAtheria.Items.Weapons.Biochemical
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
            Item.width = 20;
            Item.height = 26;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(silver: 35);
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<JarOIchorProjectile>();
            Item.shootSpeed = 15;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.damage = 21;
            Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
            Item.UseSound = SoundID.Item106;
            Item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.Bottle, 5)
                .AddIngredient(ItemID.Ichor)
                .Register();
        }
    }
}