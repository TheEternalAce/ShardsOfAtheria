using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Regular
{
    public class AmethystCore : ModItem
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/AmethystMask", EquipType.Head, this, "AmethystMask");
            }
        }

        public void SetupDrawing()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                int equipSlotHead = EquipLoader.GetEquipSlot(Mod, "AmethystMask", EquipType.Head);
                ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
                ArmorIDs.Head.Sets.DrawFullHair[equipSlotHead] = true;
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;

            SetupDrawing();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AmethystCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().amethystMask = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<AmethystCore_Lesser>().UpdateAccessory(player, hideVisual);
            player.Gem().amethystCore = true;
            player.Gem().amethystMask = !hideVisual;
        }
    }
}