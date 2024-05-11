using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Imperial
{
    [AutoloadEquip(EquipType.Legs)]
    public class ImperialGreaves : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 14;

            slotType = AreusArmorChip.SlotLegs;

            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
            player.moveSpeed += 0.16f;
            ArmorPlayer.areusLegs = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(18)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.BeetleHusk, 12)
                .AddIngredient(ItemID.Silk, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
