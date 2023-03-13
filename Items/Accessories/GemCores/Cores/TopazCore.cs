using ShardsOfAtheria.Items.Accessories.GemCores.LesserCores;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Cores
{
    public class TopazCore : ModItem
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/TopazAmulet", EquipType.Neck, this, "TopazAmulet");
            }
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TopazCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ItemID.LifeCrystal)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().topazNecklace = !hideVisual;
            player.statLifeMax2 += 40;
        }
    }
}