using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class MetalBlade : ModItem
    {
        private static SoundStyle MetalBladeSound = new SoundStyle("ShardsOfAtheria/Sounds/Item/MetalBlade");

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 9999;
            Item.AddDamageType(11);
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;
            Item.consumable = true;
            Item.maxStack = 9999;

            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged.TryThrowing();
            Item.knockBack = 3;
            Item.crit = 6;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            SoundStyle useSound = MetalBladeSound;
            if (!SoA.ServerConfig.metalBladeSound)
            {
                useSound = SoundID.Item1;
            }
            Item.UseSound = useSound;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 8;
            Item.rare = ItemDefaults.RarityCobaltMythrilAdamantite;
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