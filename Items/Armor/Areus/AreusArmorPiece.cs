using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus
{
    public abstract class AreusArmorPiece : ModItem
    {
        public int slotType;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Cyan;
        }

        public AreusArmorPlayer ArmorPlayer;
        public override void UpdateEquip(Player player)
        {
            ArmorPlayer = player.Areus();
            var chips = ArmorPlayer.chips;
            if (chips[slotType] > 0)
            {
                var item = new Item(chips[slotType]);
                if (item.ModItem is AreusArmorChip chip)
                {
                    chip.ChipEffect(player);
                }
            }

            ArmorPlayer.areusArmorPiece = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            bool melee = ArmorPlayer.classChip == DamageClass.Melee;
            bool ranged = ArmorPlayer.classChip == DamageClass.Ranged;
            bool magic = ArmorPlayer.classChip == DamageClass.Magic;
            bool summon = ArmorPlayer.classChip == DamageClass.Summon;

            if (melee)
            {

            }
            else if (magic)
            {

            }
            else if (ranged)
            {

            }
            else if (summon)
            {

            }
        }

        internal virtual void MeleeSet(Player player)
        {

        }
        internal virtual void RangedSet(Player player)
        {

        }
        internal virtual void MagicSet(Player player)
        {

        }
        internal virtual void SummonSet(Player player)
        {

        }
    }
}
