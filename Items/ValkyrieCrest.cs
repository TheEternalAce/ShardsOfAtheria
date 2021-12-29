using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.NPCs.NovaSkyloft;

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
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.maxStack = 20;
			Item.consumable = true;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddRecipeGroup("SM:GoldBars", 10)
				.AddRecipeGroup("SM:EvilMaterials", 10)
				.AddIngredient(ItemID.Feather, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<HarpyKnight>()) && !NPC.AnyNPCs(ModContent.NPCType<ValkyrieNova>());
		}

		public override bool? UseItem(Player player)
		{
			if (!player.ZoneCorrupt && !player.ZoneCrimson && Main.dayTime)
			{
				NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<HarpyKnight>());
				SoundEngine.PlaySound(SoundID.Roar, 0, 0);
				return true;
			}
			else return false;
		}
	}
}