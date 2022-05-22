using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.SlayerItems.SoulBonds
{
	public class SoulBondLunaticLord : SoulBond
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bond (Lunatic Cultist and Moon Lord)");
            Tooltip.SetDefault("Having the Lunatic Cultist and Moon Lord Soul Crystals absorbed grants the following:\n" +
                "Transforms your ice circle into a madness circle\n" +
                "The madness circle shoots madness fragments that inflict a slew of debuffs at your cursor\n" +
                "Taking damage causes several shadow tendrils to burst out of your body and damage nearby enemies");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SlayerPlayer>().LunaticSoul && player.GetModPlayer<SlayerPlayer>().LordSoul)
            {
                player.GetModPlayer<SynergyPlayer>().lunaticLordSynergy = true;
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
                .AddIngredient(ItemID.BossMaskCultist)
                .AddIngredient(ItemID.BossMaskMoonlord)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
