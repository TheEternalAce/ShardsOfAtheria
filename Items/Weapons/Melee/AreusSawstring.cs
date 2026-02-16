using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Melee.Sawstring;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusSawstring : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5);
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 76;

            Item.damage = 68;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;

            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<Swawstring>();

            SoA.TryDungeonCall("addFinesseWeapon", Type);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(20)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddIngredient(ItemID.BeetleHusk, 14)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}