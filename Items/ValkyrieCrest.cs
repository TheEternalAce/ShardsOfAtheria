using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.NPCs.NovaSkyloft;

namespace SagesMania.Items
{
	public class ValkyrieCrest : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons a holy Harpy Knight");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Red;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 45;
			item.useAnimation = 45;
			item.maxStack = 20;
			item.consumable = true;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SM:GoldBars", 10);
			recipe.AddRecipeGroup("SM:EvilMaterials", 10);
			recipe.AddIngredient(ItemID.Feather, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<HarpyKnight>()) && !NPC.AnyNPCs(ModContent.NPCType<ValkyrieNova>());
		}

		public override bool UseItem(Player player)
		{
			if (!player.ZoneCorrupt && !player.ZoneCrimson && Main.dayTime)
			{
				NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<HarpyKnight>());
				Main.PlaySound(SoundID.Roar, player.position, 0);
				return true;
			}
			else return false;
		}
	}
}