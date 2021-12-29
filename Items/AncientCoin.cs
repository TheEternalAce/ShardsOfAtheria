using ShardsOfAtheria.NPCs.Death;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
	public class AncientCoin: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons Death\n" +
				"[c/323232:'... Where did you get that..?']\n" +
				"[c/323232:'Oh, it's a fake..']\n" +
				"[c/323232:'I suppose I'll accept your challenge.']");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Red;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.consumable = true;
			Item.maxStack = 20;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddRecipeGroup("SM:Souls", 50)
				.AddTile(TileID.DemonAltar)
				.Register();
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<Death>());
		}

        public override bool? UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Death>());
			SoundEngine.PlaySound(SoundID.Roar, 0, 0);
			return true;
		}
    }
}