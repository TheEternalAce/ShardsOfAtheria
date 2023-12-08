using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class DiamondCore_Greater : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.defense = 15;

            Item.rare = ItemDefaults.RarityMechs;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DiamondCore>())
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.LargeDiamond)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().diamondShield = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<DiamondCore>().UpdateAccessory(player, hideVisual);
            player.Gem().diamondShield = !hideVisual;
            player.Gem().greaterDiamondCore = true;
        }
    }
}