using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusStaff : OverchargeWeapon
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
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

        public override void DoOverchargeEffect(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 0)
        {
            ConsumeOvercharge(player);
        }
    }
}