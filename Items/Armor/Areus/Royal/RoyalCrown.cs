using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.ShardsUI.AreusVoid;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Royal
{
    [AutoloadEquip(EquipType.Head)]
    public class RoyalCrown : AreusArmorPiece
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 20;

            slotType = AreusArmorChip.SlotHead;

            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.15f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.15f;
            ArmorPlayer.areusDamage += 0.1f;
            player.manaCost -= 0.15f;
            ArmorPlayer.areusHead = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RoyalJacket>() &&
                legs.type == ModContent.ItemType<RoyalGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            ArmorPlayer.royalSet = true;
            ModContent.GetInstance<AreusVoidSystem>().ShowBar();
            player.GetDamage(DamageClass.Generic) += ArmorPlayer.royalVoid / 100f;
            if (ArmorPlayer.CommanderSet)
            {
                player.maxMinions += 4;
            }
            base.UpdateArmorSet(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(14)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddIngredient(ItemID.FragmentStardust, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
