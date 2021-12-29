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
				"Bosses defeated will drop every possible item and will drop more than enough materials\n" +
				"A new item will drop from defeated bosses\n" +
				"Bosses defeated will never be able to summoned again\n" +
				"Enemy damage scales with max life\n" +
				"While Slayer Mode is enabled you may equip this");
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

		public override bool? UseItem(Player player)
		{
			if (!ModContent.GetInstance<SMWorld>().slayerMode)
			{
				Main.NewText("Slayer mode enabled");
				SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
				ModContent.GetInstance<SMWorld>().slayerMode = true;
			}
			else
			{
				Main.NewText("Slayer mode disabled");
				SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
				ModContent.GetInstance<SMWorld>().slayerMode = false;
			}
			return true;
		}

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (ModContent.GetInstance<SMWorld>().slayerMode)
				return true;
			else return false;
		}
    }
}