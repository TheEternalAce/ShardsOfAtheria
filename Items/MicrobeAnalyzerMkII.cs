using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
	public class MicrobeAnalyzerMkII : ModItem
	{
        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Microbe Analyzer Mk II");
			Tooltip.SetDefault("Use to analyze Microbes in inventory\n" +
				"'It's now faster and has a chance to not consume a Microbe'");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 32;
			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override bool CanUseItem(Player player)
		{
			return player.HasItem(ModContent.ItemType<UnanalyzedMicrobe>());
		}

		public override bool? UseItem(Player player)
		{
			var dropChooser = new WeightedRandom<int>();
			dropChooser.Add(ModContent.ItemType<Bacteria>());
			dropChooser.Add(ModContent.ItemType<DNA>());
			dropChooser.Add(ModContent.ItemType<Virus>());

			int microbe = Main.LocalPlayer.FindItem(ModContent.ItemType<UnanalyzedMicrobe>());
			if (Main.rand.NextFloat() >= .5f && player.inventory[microbe].stack > 0)
				player.inventory[microbe].stack--;
			if (player.inventory[microbe].stack > 0)
				Item.NewItem(player.GetSource_FromThis(), player.getRect(), dropChooser);
			return true;
		}
	}
}