using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Regular
{
    public class EmeraldCore : ModItem
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/EmeraldBoots", EquipType.Shoes, this, "EmeraldBoots");
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmeraldCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.FrostsparkBoots)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpBoost = true;
            player.wingTimeMax += 10;
            player.Shards().emeraldBoots = !hideVisual;
        }
    }
}