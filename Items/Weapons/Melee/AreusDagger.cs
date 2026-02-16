using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Melee.AreusDaggerProjs;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5, 7);
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 54;

            Item.damage = 40;
            Item.DamageType = DamageClass.Melee.TryThrowing();
            Item.knockBack = 2;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 14f;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 0, 50);
            Item.shoot = ModContent.ProjectileType<AreusDaggerProj>();

            if (Item.DamageType == DamageClass.Melee) SoA.TryDungeonCall("addFinesseWeapon", Type);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 10)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) Item.shoot = ModContent.ProjectileType<AreusDaggerCurrent>();
            else Item.shoot = ModContent.ProjectileType<AreusDaggerProj>();
            return base.CanUseItem(player);
        }
    }
}