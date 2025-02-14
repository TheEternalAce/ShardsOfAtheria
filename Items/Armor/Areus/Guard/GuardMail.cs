using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Guard
{
    [AutoloadEquip(EquipType.Body)]
    public class GuardMail : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 7;

            slotType = AreusArmorChip.SlotChest;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.05f;
            player.statManaMax2 += 60;
            ArmorPlayer.areusDamage += 0.03f;
            ArmorPlayer.areusBody = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(24)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient<Jade>(10)
                .AddIngredient<SoulOfSpite>(12)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
