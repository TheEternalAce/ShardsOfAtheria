using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaFragmentA : DecaFragment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deca Fragment");
            Tooltip.SetDefault("Increased power");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Red;
            item.width = 26;
            item.height = 26;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<DecaPlayer>().decaFragmentA = true;
        }
    }
}