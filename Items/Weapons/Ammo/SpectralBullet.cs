using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class SpectralBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            Item.AddElementAqua();
            Item.AddRedemptionElement(14);
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;

            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
            Item.shoot = ModContent.ProjectileType<SpectralBulletProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ItemID.EmptyBullet, 250)
                .AddIngredient(ItemID.Ectoplasm)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}