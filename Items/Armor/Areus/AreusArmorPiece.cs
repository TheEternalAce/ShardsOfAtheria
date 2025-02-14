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
        public float lesserNonSetDamage = 0f;

        public AreusArmorPlayer ArmorPlayer;

        public override ModItem Clone(Item newEntity)
        {
            var clone = (AreusArmorPiece)base.Clone(newEntity);
            clone.ArmorPlayer = null;
            return clone;
        }

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
                        chip.UpdateChip(player);
                    }
                }
            }
        }

        public override void UpdateArmorSet(Player player)
        {
            ArmorPlayer = player.Areus();
            string itemKey = this.GetLocalizationKey(string.Empty);
            string setBonusText = ShardsHelpers.Localize("SetBonus.Areus");

            bool melee = ArmorPlayer.WarriorSetChip;
            bool ranged = ArmorPlayer.RangerSetChip;
            bool magic = ArmorPlayer.MageSetChip;
            bool summon = ArmorPlayer.CommanderSetChip;
            bool throwing = ArmorPlayer.NinjaSetChip;

            setBonusText += "\n" + Language.GetTextValue(itemKey + "SetBonus");

            if (melee || ranged || magic || summon || throwing)
            {
                itemKey += ArmorPlayer.classChip.Name.Replace("DamageClass", "") + "Bonus";
                setBonusText += "\n" + Language.GetTextValue(itemKey);
            }
            if (melee) MeleeSet(player);
            else if (ranged) RangedSet(player);
            else if (magic) MagicSet(player);
            else if (summon) SummonSet(player);
            else if (throwing) ThrowingSet(player);
            player.setBonus = setBonusText;
        }

        public virtual void MeleeSet(Player player)
        {
            player.statDefense += 15;
            player.GetDamage(DamageClass.Ranged) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Magic) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Summon) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Throwing) += lesserNonSetDamage;
        }

        public virtual void RangedSet(Player player)
        {
            player.moveSpeed += 0.5f;
            player.GetDamage(DamageClass.Melee) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Magic) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Summon) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Throwing) += lesserNonSetDamage;
        }

        public virtual void MagicSet(Player player)
        {
            player.manaCost -= 0.2f;
            player.manaRegenBonus += 20;
            player.GetDamage(DamageClass.Melee) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Ranged) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Summon) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Throwing) += lesserNonSetDamage;
        }

        public virtual void SummonSet(Player player)
        {
            player.maxMinions += 2;
            player.GetDamage(DamageClass.Melee) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Ranged) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Magic) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Throwing) += lesserNonSetDamage;
        }

        public virtual void ThrowingSet(Player player)
        {
            ArmorPlayer.ninjaSet = true;
            player.GetDamage(DamageClass.Melee) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Ranged) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Magic) += lesserNonSetDamage;
            player.GetDamage(DamageClass.Summon) += lesserNonSetDamage;
        }
    }
}
