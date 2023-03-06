using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusKatana : OverchargeWeapon
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.UpgradeableItem.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;

            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.shootSpeed = 6;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 1, 50);
            Item.shoot = ModContent.ProjectileType<ElectricKunai>();
            chargeAmount = 0.25f;
            overchargeShoot = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 17)
                .AddRecipeGroup(ShardsRecipes.Gold, 5)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.Overcharged().overcharged)
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
            }
            else
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<OverchargePlayer>().overcharged)
            {
                return false;
            }
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(10);
            position += Vector2.Normalize(velocity) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage / 3, knockback, player.whoAmI);
            }
            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
        }

        public override void DoOverchargeEffect(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 0)
        {
            velocity.Normalize();
            base.DoOverchargeEffect(player, ModContent.ProjectileType<AreusKatanaProj>(), 4f, velocity, 0f);
        }
    }
}