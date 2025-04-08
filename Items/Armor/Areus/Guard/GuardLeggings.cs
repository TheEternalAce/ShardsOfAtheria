using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Guard
{
    [AutoloadEquip(EquipType.Legs)]
    public class GuardLeggings : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 6;

            slotType = AreusArmorChip.SlotLegs;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.05f;
            ArmorPlayer.areusDamage += 0.03f;
            ArmorPlayer.areusLegs = true;
            player.moveSpeed += 0.08f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(18)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient<Jade>(6)
                .AddIngredient<SoulOfTwilight>(12)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
