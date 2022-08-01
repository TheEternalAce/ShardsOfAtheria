using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.SlayerItems.SoulBonds
{
    public class SoulBondEyeTwin : SoulBond
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bond (Eye of Cthulhu and The Twins)");
            Tooltip.SetDefault("Transforms All Seeing Eye into a Mechanical All Seeing Eye\n" +
                "Mechanical All Seeing Eye has increased radius and fires lasers at enemies in the radius\n" +
                "Marked enemies take 25% more damage instead of 10%");

            base.SetStaticDefaults();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SlayerPlayer>().EyeSoul && player.GetModPlayer<SlayerPlayer>().TwinSoul)
            {
                player.GetModPlayer<SynergyPlayer>().eyeTwinSynergy = true;
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
                .AddIngredient(ItemID.TwinMask)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
