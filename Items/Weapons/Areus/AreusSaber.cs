using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Areus.AreusSaber;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusSaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 54;

            Item.damage = 246;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 4f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 57);
            Item.shoot = ModContent.ProjectileType<AreusSlash1>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusDagger>(), 1)
                .AddIngredient(ModContent.ItemType<AreusSword>(), 1)
                .AddIngredient(ItemID.LunarBar, 14)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}