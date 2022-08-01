using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class ModelDeca : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Model Deca");
            Tooltip.SetDefault("'The new Death has been made, and soon a new dawn shall rise'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.accessory = true;

            base.SetDefaults();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DecaPlayer>().modelDeca = true;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DecaShardProj>()] < player.GetModPlayer<DecaPlayer>().decaShards)
                Projectile.NewProjectileDirect(player.GetSource_Accessory(Item), player.Center + Vector2.One.RotatedBy(MathHelper.ToRadians(360))*45, Vector2.Zero,
                    ModContent.ProjectileType<DecaShardProj>(), 200000, 1f, player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DecaFragmentA>())
                .AddIngredient(ModContent.ItemType<DecaFragmentB>())
                .AddIngredient(ModContent.ItemType<DecaFragmentC>())
                .AddIngredient(ModContent.ItemType<DecaFragmentD>())
                .AddIngredient(ModContent.ItemType<DecaFragmentE>())
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}