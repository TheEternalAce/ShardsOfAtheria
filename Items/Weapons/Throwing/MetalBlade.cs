using Microsoft.Xna.Framework;
using ShardsOfAtheria.Config;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Throwing;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Throwing
{
    public class MetalBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 999;

            WeaponElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;
            Item.consumable = true;
            Item.maxStack = 9999;

            Item.damage = 60;
            Item.DamageType = DamageClass.Throwing;
            Item.knockBack = 3;
            Item.crit = 6;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = ModContent.GetInstance<ShardsServerConfig>().metalBladeSound ? new SoundStyle("ShardsOfAtheria/Sounds/Item/MetalBlade") : SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 8;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 6800;
            Item.shoot = ModContent.ProjectileType<MetalBladeProj>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe(300)
                .AddIngredient(ItemID.TitaniumBar, 5)
                .AddRecipeGroup(RecipeGroupID.IronBar, 3)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}