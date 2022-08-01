using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.SlayerItems.SoulBonds
{
    public class SoulBondKingQueen : SoulBond
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bond (King Slime and Queen Slime)");
            Tooltip.SetDefault("Having the King Slime and Queen Slime Soul Crystals absorbed grants the following:\n" +
                "Summons a temporary slime minion to fight along side you after taking 100 damage\n" +
                "You may have up to 10 of these slimes");

            base.SetStaticDefaults();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SlayerPlayer>().KingSoul && player.GetModPlayer<SlayerPlayer>().QueenSoul)
            {
                player.GetModPlayer<SynergyPlayer>().kingQueenSynergy = true;
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
                .AddIngredient(ItemID.KingSlimeMask)
                .AddIngredient(ItemID.QueenSlimeMask)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
