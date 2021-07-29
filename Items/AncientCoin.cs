using SagesMania.NPCs.Death;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
	public class AncientCoin: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons Death\n" +
				"[c/323232:''... Where did you get that..?'']\n" +
				"[c/323232:''Oh, it's a fake..'']\n" +
				"[c/323232:''I suppose I'll accept your challenge.'']");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Red;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 45;
			item.useAnimation = 45;
			item.consumable = true;
			item.maxStack = 20;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SM:Souls", 50);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<Death>());
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Death>());
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
	}
}