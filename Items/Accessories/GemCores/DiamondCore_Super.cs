using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class DiamondCore_Super : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Diamond Core");
			Tooltip.SetDefault("Immune to slimes and defense reducing debuffs\n" +
				"Ankh Shield effect");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.defense = 15;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<DiamondCore_Greater>())
				.AddIngredient(ItemID.FragmentSolar, 5)
				.AddIngredient(ItemID.FragmentVortex, 5)
				.AddIngredient(ItemID.AnkhShield)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.npcTypeNoAggro[1] = true;
			player.npcTypeNoAggro[16] = true;
			player.npcTypeNoAggro[59] = true;
			player.npcTypeNoAggro[71] = true;
			player.npcTypeNoAggro[81] = true;
			player.npcTypeNoAggro[138] = true;
			player.npcTypeNoAggro[121] = true;
			player.npcTypeNoAggro[122] = true;
			player.npcTypeNoAggro[141] = true;
			player.npcTypeNoAggro[147] = true;
			player.npcTypeNoAggro[183] = true;
			player.npcTypeNoAggro[184] = true;
			player.npcTypeNoAggro[204] = true;
			player.npcTypeNoAggro[225] = true;
			player.npcTypeNoAggro[244] = true;
			player.npcTypeNoAggro[302] = true;
			player.npcTypeNoAggro[333] = true;
			player.npcTypeNoAggro[335] = true;
			player.npcTypeNoAggro[334] = true;
			player.npcTypeNoAggro[336] = true;
			player.npcTypeNoAggro[537] = true;

			player.buffImmune[BuffID.Poisoned] = true;
			player.buffImmune[BuffID.Bleeding] = true;
			player.buffImmune[BuffID.Darkness] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.buffImmune[BuffID.Silenced] = true;
			player.buffImmune[BuffID.Slow] = true;
			player.buffImmune[BuffID.Confused] = true;
			player.buffImmune[BuffID.BrokenArmor] = true;
			player.buffImmune[BuffID.Weak] = true;
			player.noKnockback = true;
			player.fireWalk = true;

			player.buffImmune[BuffID.WitheredArmor] = true;
			player.buffImmune[BuffID.Ichor] = true;
		}
	}
}