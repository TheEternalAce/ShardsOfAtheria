using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Magic
{
    public class Gunsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("''I think it's broken..''");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.damage = 30;
            item.magic = true;
            item.crit = 16;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 16;
            item.mana = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (WorldGen.crimson)
                type = ModContent.ProjectileType<GunCrimson>();
            else type = ModContent.ProjectileType<GunCorruption>();
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddRecipeGroup("SM:EvilGuns");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}