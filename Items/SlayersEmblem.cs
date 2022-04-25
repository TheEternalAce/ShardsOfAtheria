using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
	public class SlayersEmblem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Slayer's Emblem");
			Tooltip.SetDefault("Use to toggle Slayer mode\n" +
				"Bosses considered irreplaceable will also drop every possible item, in a stack of 999 if the item is stackable, its slayer mode exclusive item and its Soul Crystal\n" +
				"Using a boss's Soul Crystal will prevent that boss from ever spawning again and grants the player its powers\n" +
				"Immunity frames disabled\n" +
                "Cannot be used while a boss is alive");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Red;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.accessory = true;
			Item.vanity = true;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddTile(TileID.DemonAltar)
				.Register();
		}

        public override bool CanUseItem(Player player)
        {
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.life > 0 && npc.boss)
					return false;
			}
			return true;
		}

        public override bool? UseItem(Player player)
		{
			if (!ModContent.GetInstance<SoAWorld>().slayerMode)
			{
				Main.NewText("Slayer mode enabled");
				SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
				ModContent.GetInstance<SoAWorld>().slayerMode = true;
			}
			else
			{
				Main.NewText("Slayer mode disabled");
				SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
				ModContent.GetInstance<SoAWorld>().slayerMode = false;
			}
			return true;
		}
    }
}