using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.NPCs.NovaStellar;

namespace ShardsOfAtheria.Items
{
	public class ValkyrieCrest : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons a holy Harpy Knight");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Red;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.buyPrice(0, 5);
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.maxStack = 20;
			Item.consumable = true;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddRecipeGroup(SoARecipes.Gold, 10)
				.AddRecipeGroup(SoARecipes.EvilMaterial, 10)
				.AddIngredient(ItemID.Feather, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}

		// We use the CanUseItem hook to prevent a Player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player Player)
		{
			return Player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<NovaStellar>());
		}

		public override bool? UseItem(Player Player)
		{
			if (Player.whoAmI == Main.myPlayer)
			{
				// If the Player using the item is the client
				// (explicitely excluded serverside here)
				SoundEngine.PlaySound(SoundID.Roar, Player.position, 0);

				int type = ModContent.NPCType<NovaStellar>();

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					// If the Player is not in multiPlayer, spawn directly
					NPC.SpawnOnPlayer(Player.whoAmI, type);
				}
				else
				{
					// If the Player is in multiPlayer, request a spawn
					// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
					NetMessage.SendData(MessageID.SpawnBoss, number: Player.whoAmI, number2: type);
				}
			}

			return !(Player.ZoneCorrupt || Player.ZoneCrimson) && Main.dayTime;
		}
	}
}