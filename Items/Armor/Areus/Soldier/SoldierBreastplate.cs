using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Soldier
{
    [AutoloadEquip(EquipType.Body)]
    public class SoldierBreastplate : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.defense = 14;

            slotType = AreusArmorChip.SlotChest;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.08f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.08f;
            ArmorPlayer.areusDamage += 0.05f;
            player.statManaMax2 += 90;
            ArmorPlayer.areusBody = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(24)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.SoulofNight, 12)
                .AddIngredient(ItemID.Silk, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
