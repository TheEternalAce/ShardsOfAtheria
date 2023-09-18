using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class BloodScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElementAqua();
            Item.AddElementWood();
            SoAGlobalItem.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 76;
            Item.scale = 3f;

            Item.damage = 300;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 96;

            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemRarityID.Red;
            Item.value = 321000;
            Item.shoot = ModContent.ProjectileType<DeathScythe>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}