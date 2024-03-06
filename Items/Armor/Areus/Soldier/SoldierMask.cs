using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Soldier
{
    [AutoloadEquip(EquipType.Head)]
    public class SoldierMask : AreusArmorPiece
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 12;
            lesserNonSetDamage = 0.2f;

            slotType = AreusArmorChip.SlotHead;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.08f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.08f;
            ArmorPlayer.areusDamage += 0.05f;
            player.manaCost -= 0.08f;
            ArmorPlayer.areusHead = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SoldierBreastplate>() &&
                legs.type == ModContent.ItemType<SoldierLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            ArmorPlayer.soldierSet = true;
            if (ArmorPlayer.CommanderSet)
            {
                player.maxMinions += 3;
            }
            base.UpdateArmorSet(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(14)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddIngredient(ItemID.SoulofLight, 12)
                .AddIngredient(ItemID.Silk, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
