using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusGrenade : OverchargeWeapon
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 999;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 56;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.ammo = ItemID.Grenade;

            Item.damage = 70;
            Item.DamageType = DamageClass.Throwing;
            Item.knockBack = 7;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 16f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<AreusGrenadeProj>();
            overchargeShoot = false;
        }

        public override void DoOverchargeEffect(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 1)
        {
            base.DoOverchargeEffect(player, ModContent.ProjectileType<AreusGrenadeProj>(), damageMultiplier, velocity, ai1);
        }

        public override void AddRecipes()
        {
            CreateRecipe(60)
                .AddIngredient(ModContent.ItemType<AreusShard>(), 3)
                .AddIngredient(ItemID.GoldBar)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 2)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }
    }
}