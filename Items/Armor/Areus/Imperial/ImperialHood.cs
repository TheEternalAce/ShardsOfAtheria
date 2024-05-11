using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.ShardsUI.AreusVoid;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Imperial
{
    [AutoloadEquip(EquipType.Head)]
    public class ImperialHood : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 14;
            lesserNonSetDamage = 0.15f;

            slotType = AreusArmorChip.SlotHead;

            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
            ArmorPlayer.areusHead = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ImperialGarments>() &&
                legs.type == ModContent.ItemType<ImperialGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
            ArmorPlayer.imperialSet = true;
            ModContent.GetInstance<AreusVoidSystem>().ShowBar();
            player.GetDamage(DamageClass.Generic) += ArmorPlayer.imperialVoid / 100f;
            if (ArmorPlayer.CommanderSet)
            {
                player.maxMinions += 1;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(14)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddIngredient(ItemID.BeetleHusk, 12)
                .AddIngredient(ItemID.Silk, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
