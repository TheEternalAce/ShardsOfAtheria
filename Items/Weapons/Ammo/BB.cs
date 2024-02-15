using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class BB : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 1.5f;

            Item.rare = ItemDefaults.RarityPreBoss;
            Item.value = 1; //ItemDefaults.ValueEyeOfCthulhu; formerly 50 silver, rest in piece
            Item.shoot = ModContent.ProjectileType<BBProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
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