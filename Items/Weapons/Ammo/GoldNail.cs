using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class GoldNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3f;

            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
            Item.shoot = ModContent.ProjectileType<GoldenNail>();
            Item.shootSpeed = 6f;
            Item.ammo = AmmoID.NailFriendly;
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddRecipeGroup(ShardsRecipes.Copper)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}