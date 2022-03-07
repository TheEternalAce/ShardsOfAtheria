using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class Gunsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'I think it's broken..'");
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.crit = 16;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16;
            Item.mana = 6;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (WorldGen.crimson)
                type = ModContent.ProjectileType<GunCrimson>();
            else type = ModContent.ProjectileType<GunCorruption>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBroadsword)
                .AddRecipeGroup(SoARecipes.EvilGun)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}