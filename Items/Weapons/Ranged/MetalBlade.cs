using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class MetalBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.ThrowingWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;
            Item.consumable = true;
            Item.maxStack = 9999;

            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3;
            Item.crit = 6;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = ModContent.GetInstance<ShardsConfigServerSide>().metalBladeSound ? new SoundStyle($"{nameof(ShardsOfAtheria)}/Sounds/Item/MetalBlade") : SoundID.Item1;
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

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<MetalBladeProjStick>();
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<MetalBladeProj>();
            }
            return base.CanUseItem(player);
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