using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.SlayerItems.SoulBonds
{
	public class SoulBondBrainEye : SoulBond
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bond (Brain of Cthulhu and Eye of Cthulhu)");
            Tooltip.SetDefault("Having the Eye of Cthulhu and Brain of Cthulhu Soul Crystals absorbed grants the following:\n" +
                "Passive Dangersense, Hunter, Night Owl, Shine and Spelunker buffs\n" +
                "Any Servants out will also provide immunity and prevent attacking");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SlayerPlayer>().EyeSoul && player.GetModPlayer<SlayerPlayer>().BrainSoul)
            {
                player.GetModPlayer<SynergyPlayer>().brainEyeSynergy = true;
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
                .AddIngredient(ItemID.EyeMask)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
