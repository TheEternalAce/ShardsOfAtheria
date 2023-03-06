using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus.AreusSword;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusSword : OverchargeWeapon
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.UpgradeableItem.Add(Type);
            SoAGlobalItem.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 76;

            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<AreusSwordProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddRecipeGroup(ShardsRecipes.Gold, 6)
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }

        public override void DoOverchargeEffect(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 0)
        {
            float numberProjectiles = 5;
            float shardRotation = MathHelper.ToRadians(15);
            Vector2 position = player.Center;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-shardRotation, shardRotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(player.GetSource_FromThis(), position, perturbedSpeed, ModContent.ProjectileType<ElectricBlade>(),
                    (int)(Item.damage * damageMultiplier), Item.knockBack, player.whoAmI);
            }
            ConsumeOvercharge(player);
        }
    }
}