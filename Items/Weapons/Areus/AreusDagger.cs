using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusDagger : ModItem
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

            Item.damage = 52;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 4f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 0, 50);
            Item.shoot = ModContent.ProjectileType<AreusDaggerProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 7)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 10)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}