using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class SpectralArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 999;
            WeaponElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 1200;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<SpectralArrowProj>();
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ItemID.WoodenArrow, 250)
                .AddIngredient(ItemID.Ectoplasm)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}