using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
	public class MicrobeAnalyzerMkII : ModItem
	{
        public override string Texture => "ShardsOfAtheria/Items/MicrobeAnalyzer";
        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Microbe Analyzer Mk II");
			Tooltip.SetDefault("Use to analyze Microbes in inventory\n" +
				"'It's now faster and has a chance to not consume a Microbe'");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<MicrobeAnalyzer>());
			Item.useTime = 0;
			Item.useAnimation = 0;
		}

        public override bool CanConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .5f;
        }

        public override bool? UseItem(Player player)
		{
			var dropChooser = new WeightedRandom<int>();
			dropChooser.Add(ModContent.ItemType<Bacteria>());
			dropChooser.Add(ModContent.ItemType<DNA>());
			dropChooser.Add(ModContent.ItemType<Virus>());
			

			Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<MicrobeAnalyzerMkII>()), player.getRect(), dropChooser);
			return base.UseItem(player);
        }
    }
}