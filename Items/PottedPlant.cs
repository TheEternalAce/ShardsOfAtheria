using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;

namespace ShardsOfAtheria.Items
{
	public class PottedPlant : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons Plantera");
			ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; // This helps sort inventory know that this is a boss summoning Item.

			// This is set to true for all NPCs that can be summoned via an Item (calling NPC.SpawnOnPlayer). If this is for a modded boss,
			// write this in the bosses file instead
			NPCID.Sets.MPAllowedEnemies[NPCID.Plantera] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Red;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.maxStack = 9999;
			Item.consumable = true;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ItemID.Moonglow, 5)
				.AddIngredient(ItemID.Vine, 6)
				.AddIngredient(ItemID.ChlorophyteBar, 4)
				.AddIngredient(ItemID.SoulofFright)
				.AddIngredient(ItemID.SoulofMight)
				.AddIngredient(ItemID.SoulofSight)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		// We use the CanUseItem hook to prevent a Player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) && player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);
		}

		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				// If the Player using the item is the client
				// (explicitely excluded serverside here)
				SoundEngine.PlaySound(SoundID.Roar, player.position);

				int type = NPCID.Plantera;

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					// If the Player is not in multiPlayer, spawn directly
					NPC.SpawnOnPlayer(player.whoAmI, type);
				}
				else
				{
					// If the Player is in multiPlayer, request a spawn
					// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
					NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
				}
			}

			return player.ZoneRockLayerHeight && player.ZoneJungle;
		}
	}
}
