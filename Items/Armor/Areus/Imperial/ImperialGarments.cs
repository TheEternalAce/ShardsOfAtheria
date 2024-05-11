using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Imperial
{
    [AutoloadEquip(EquipType.Body)]
    public class ImperialGarments : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 26;

            slotType = AreusArmorChip.SlotChest;

            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
            ArmorPlayer.areusBody = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(24)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.BeetleHusk, 12)
                .AddIngredient(ItemID.Silk, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
