using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class EmeraldCore_Greater : ModItem
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

            Item.rare = ItemDefaults.RarityMechs;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmeraldCore>())
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().emeraldBoots = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<EmeraldCore>().UpdateAccessory(player, hideVisual);
            player.Gem().greaterEmeraldCore = true;
            player.wingTimeMax += 10;
        }
    }
}