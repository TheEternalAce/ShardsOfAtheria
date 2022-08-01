using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.SlayerItems.SoulBonds
{
    public class SoulBondEyeLord : SoulBond
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bond (Eye of Cthulhu and Moon Lord)");
            Tooltip.SetDefault("Having the Eye of Cthulhu and Moon Lord Soul Crystals absorbed grants the following:\n" +
                "Transforms All Seeing Eye into a Moon Eye\n" +
                "Moon Eye has increased radius and fired Phantasmal Eyes at enemies in the radius\n" +
                "Marked enemies take 50% more damage instead of 10%");

            base.SetStaticDefaults();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SlayerPlayer>().EyeSoul && player.GetModPlayer<SlayerPlayer>().LordSoul)
            {
                player.GetModPlayer<SynergyPlayer>().eyeLordSynergy = true;
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
                .AddIngredient(ItemID.EyeMask)
                .AddIngredient(ItemID.BossMaskMoonlord)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
