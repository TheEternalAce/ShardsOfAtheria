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
            bool melee = ArmorPlayer.classChip == DamageClass.Melee;
            bool ranged = ArmorPlayer.classChip == DamageClass.Ranged;
            bool magic = ArmorPlayer.classChip == DamageClass.Magic;
            bool summon = ArmorPlayer.classChip == DamageClass.Summon;

            if (melee)
            {
                MeleeSet(player);
            }
            else if (magic)
            {
                MagicSet(player);
            }
            else if (ranged)
            {
                RangedSet(player);
            }
            else if (summon)
            {
                SummonSet(player);
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
