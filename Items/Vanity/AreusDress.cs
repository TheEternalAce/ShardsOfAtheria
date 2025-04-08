using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    public class AreusDress : ModItem
    {
        public override void Load()
        {
            // Since the equipment textures weren't loaded on the server, we can't have this code running server-side
            if (Main.netMode == NetmodeID.Server)
                return;
            // Add equip textures
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
        }

        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 62;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityBossMasks;
            Item.value = ItemDefaults.ValueBossMasks;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddIngredient<AreusShard>(10)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddTile<AreusFabricator>()
                .AddTile(TileID.Loom)
                .Register();
        }

        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            // By changing the equipSlot to the leg equip texture slot, the leg texture will now be drawn on the player
            // We're changing the leg slot so we set this to true
            robes = true;
            // Here we can get the equip slot by name since we referenced the item when adding the texture
            // You can also cache the equip slot in a variable when you add it so this way you don't have to call GetEquipSlot
            equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
        }
    }
}
