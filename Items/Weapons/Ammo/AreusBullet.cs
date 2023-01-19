using MMZeroElements;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class AreusBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 999;
            SoAGlobalItem.AreusWeapon.Add(Type);
            WeaponElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 1200;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<AreusBulletProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.MusketBall, 100)
                .AddIngredient(ModContent.ItemType<AreusShard>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}