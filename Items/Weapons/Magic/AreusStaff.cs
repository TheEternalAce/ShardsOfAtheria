using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 56;

            Item.damage = 130;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3.75f;
            Item.crit = 16;
            Item.mana = 6;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.staff[Item.type] = true;

            Item.shootSpeed = 12f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<ElectricOrb>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddRecipeGroup(ShardsRecipes.Gold, 3)
                .AddIngredient(ItemID.FragmentVortex, 7)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }
    }
}