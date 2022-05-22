using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.SlayerItems.SoulBonds
{
	public class SoulBondMechaMayhem : SoulBond
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bond (Mecha Mayhem)");
            Tooltip.SetDefault("Having The Destroyer, Skeletron Prime and The Twins Soul Crystals absorbed grants the following:\n" +
                "Destroyer Probes spawn every 10 seconds\n" +
                "You can have up to 5 of these Destroyer Probes\n" +
                "Reduces \"spin phase\" time to activate to 20 seconds\n" +
                "Shadow Double deals 60 damage");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SlayerPlayer>().DestroyerSoul && player.GetModPlayer<SlayerPlayer>().PrimeSoul && player.GetModPlayer<SlayerPlayer>().TwinSoul)
            {
                player.GetModPlayer<SynergyPlayer>().mechaMayhemSynergy = true;
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
                .AddIngredient(ItemID.DestroyerMask)
                .AddIngredient(ItemID.SkeletronPrimeMask)
                .AddIngredient(ItemID.TwinMask)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
