using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class SpectralBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 999;
            WeaponElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 1200;
            Item.rare = ItemRarityID.Lime;
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