using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusDaggerProjs;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.UpgradeableItem.Add(Type);
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
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 14f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 0, 50);
            Item.shoot = ModContent.ProjectileType<AreusDaggerProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddRecipeGroup(ShardsRecipes.Gold, 5)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 10)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<AreusDaggerCurrent>();
                return player.ownedProjectileCounts[ModContent.ProjectileType<AreusDaggerProj>()] > 0;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<AreusDaggerProj>();
                return player.ownedProjectileCounts[ModContent.ProjectileType<AreusDaggerProj>()] < 8;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<AreusDaggerProj>()] > 0)
                {
                    var daggerIndex = FindOldestDagger();
                    if (daggerIndex != -1)
                    {
                        var dagger = Main.projectile[daggerIndex];

                        var daggerCenter = dagger.Center;
                        var vector = Vector2.Normalize(daggerCenter - player.Center) * 16;

                        Projectile.NewProjectile(source, position, vector,
                            ModContent.ProjectileType<AreusDaggerCurrent>(),
                            damage, knockback, player.whoAmI, daggerIndex);
                    }
                }
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public static int FindOldestDagger()
        {
            int result = -1;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile otherDagger = Main.projectile[i];
                if (otherDagger.active && otherDagger.type == ModContent.ProjectileType<AreusDaggerProj>())
                {
                    var dagger = (otherDagger.ModProjectile as AreusDaggerProj);
                    if (dagger.IsStickingToTarget)
                    {
                        result = i;
                        break;
                    }
                }
            }
            return result;
        }
    }
}