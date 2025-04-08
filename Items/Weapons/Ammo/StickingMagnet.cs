using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class StickingMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            Item.AddDamageType(7);
            Item.AddRedemptionElement(5);
            Item.AddRedemptionElement(13);
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = 1000;
            Item.shoot = ModContent.ProjectileType<StickingMagnetProj>();
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            CreateRecipe(3)
                .AddRecipeGroup(RecipeGroupID.IronBar)
                .AddIngredient(ItemID.Bone)
                .AddIngredient(ItemID.Wire)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}