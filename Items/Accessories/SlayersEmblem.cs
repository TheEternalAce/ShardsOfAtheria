using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class SlayersEmblem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Slayer's Emblem");
			Tooltip.SetDefault("Toggles Slayer mode\n" +
				"Bosses defeated will drop every possible item and will drop more than enough materials\n" +
				"A new item will drop from defeated bosses\n" +
				"Bosses defeated will never be able to summoned again\n" +
				"While Slayer Mode is enabled you may equip this\n" +
				"5% increased damage\n" +
				"This item's effect is not fully implemented yet");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Red;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 45;
			item.useAnimation = 45;
			item.accessory = true;
			item.vanity = true;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool UseItem(Player player)
		{
			if (!ModContent.GetInstance<SMWorld>().slayerMode)
			{
				Main.NewText("Slayer mode enabled");
				Main.PlaySound(SoundID.Roar, player.position, 0);
				ModContent.GetInstance<SMWorld>().slayerMode = true;
			}
			else
			{
				Main.NewText("Slayer mode disabled");
				Main.PlaySound(SoundID.Roar, player.position, 0);
				ModContent.GetInstance<SMWorld>().slayerMode = false;
			}
			return true;
		}

        public override bool CanEquipAccessory(Player player, int slot)
        {
			if (ModContent.GetInstance<SMWorld>().slayerMode)
				return true;
			else return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().slayersEmblem = true;
		}
    }
}