using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulBonds
{
	public class SoulBondBrainLord : SoulBond
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bond (Brain of Cthulhu and Moon Lord)");
            Tooltip.SetDefault("Having the Brain of Cthulhu and Moon Lord Soul Crystals absorbed grants the following:\n" +
                "Transforms your Creepers into True Creepers\n" +
                "Allows attacking with True Creepers active at the cost of 50% reduced damage");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SlayerPlayer>().BrainSoul && player.GetModPlayer<SlayerPlayer>().LordSoul)
            {
                player.GetModPlayer<SynergyPlayer>().brainLordSynergy = true;
            }
		}

		public override void RightClick(Player player)
		{
			// In order to preserve its expected behavior (right click swaps this and a different currently equipped accessory)
			// we need to call the parent method via base.Method(arguments)
			// Removing this line will cause this item to just vanish when right clicked
			base.RightClick(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BrainMask)
                .AddIngredient(ItemID.BossMaskMoonlord)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
