using BloodIsFuel;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Guard
{
    [AutoloadEquip(EquipType.Head)]
    public class GuardHelmet : AreusArmorPiece
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.defense = 6;

            slotType = AreusArmorChip.SlotHead;

            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.05f;
            player.manaCost -= 0.05f;
            ArmorPlayer.areusDamage += 0.03f;
            ArmorPlayer.areusHead = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(14)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddIngredient<SoulOfDaylight>(12)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GuardMail>() &&
                legs.type == ModContent.ItemType<GuardLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            ArmorPlayer.guardSet = true;
            ModContent.GetInstance<AreusEnergySystem>().ShowBar();
            base.UpdateArmorSet(player);
        }

        public override void MeleeSet(Player player)
        {
            base.MeleeSet(player);
        }

        public override void RangedSet(Player player)
        {
            base.RangedSet(player);
        }

        public override void MagicSet(Player player)
        {
            if (++ArmorPlayer.energyTimer >= AreusArmorPlayer.EnergyTimerMax)
            {
                ArmorPlayer.areusEnergy += 3;
                ArmorPlayer.energyTimer = 0;
            }
        }

        public override void SummonSet(Player player)
        {
            if (player.InCombat())
            {
                if (++ArmorPlayer.energyTimer >= AreusArmorPlayer.EnergyTimerMax)
                {
                    ArmorPlayer.areusEnergy++;
                    ArmorPlayer.energyTimer = 0;
                }
            }
        }
    }
}
