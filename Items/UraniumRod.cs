using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class UraniumRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'You feel pretty sick'");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.Blue;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.shoot = ModContent.ProjectileType<UraniumRodProj>();
            Item.shootSpeed = 16f;
            Item.ammo = ModContent.ItemType<AreusRod>();
        }

        public override void UpdateInventory(Player player)
        {
            player.AddBuff(ModContent.BuffType<ModerateRadiationPoisoning>(), 600);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UraniumOre>(), 3)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}