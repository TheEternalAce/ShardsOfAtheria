using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus
{
    public abstract class AreusArmorPiece : ModItem
    {
        public int slotType;

        public AreusArmorPlayer ArmorPlayer;
        public override void UpdateEquip(Player player)
        {
            ArmorPlayer = player.Areus();
            var chips = ArmorPlayer.chipNames;
            if (chips[slotType] != "")
            {
                var pendingChip = chips[slotType];
                if (SoA.Instance.TryFind<ModItem>(pendingChip, out var item))
                {
                    if (item is AreusArmorChip chip)
                    {
                        chip.ChipEffect(player);
                    }
                }
            }

            ArmorPlayer.areusArmorPiece = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            string itemKey = this.GetLocalizationKey(string.Empty);
            string setBonusText = Language.GetTextValue(SoA.LocalizeSetBonus + "Areus");

            bool melee = ArmorPlayer.WarriorSet;
            bool ranged = ArmorPlayer.RangerSet;
            bool magic = ArmorPlayer.MageSet;
            bool summon = ArmorPlayer.CommanderSet;

            setBonusText += "\n" + Language.GetTextValue(itemKey + "SetBonus");

            if (melee || ranged || magic || summon)
            {
                itemKey += ArmorPlayer.classChip.Name + "Bonus";
                setBonusText += "\n" + Language.GetTextValue(itemKey);
            }
            if (melee)
            {
                MeleeSet(player);
                setBonusText += "\n" + Language.GetTextValue(SoA.LocalizeSetBonus + "AreusMelee");
            }
            else if (ranged)
            {
                RangedSet(player);
                setBonusText += "\n" + Language.GetTextValue(SoA.LocalizeSetBonus + "AreusRanged");
            }
            else if (magic)
            {
                MagicSet(player);
                setBonusText += "\n" + Language.GetTextValue(SoA.LocalizeSetBonus + "AreusMagic");
            }
            else if (summon)
            {
                SummonSet(player);
                setBonusText += "\n" + Language.GetTextValue(SoA.LocalizeSetBonus + "AreusSummon");
            }
            player.setBonus = setBonusText;
        }

        public virtual void MeleeSet(Player player)
        {

        }
        public virtual void RangedSet(Player player)
        {

        }
        public virtual void MagicSet(Player player)
        {

        }
        public virtual void SummonSet(Player player)
        {

        }
    }
}
