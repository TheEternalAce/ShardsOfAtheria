using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class RubyCore : ModItem
	{
		public override void Load()
		{
			if (Main.netMode != NetmodeID.Server)
			{
				EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/RubyGauntlet", EquipType.HandsOn, this, "RubyGauntlet");
				EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/RubyGauntlet_Off", EquipType.HandsOff, this, "RubyGauntlet_Off");
			}
		}

		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1, 25);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<RubyCore_Lesser>())
				.AddIngredient(ItemID.HellstoneBar, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.ShardsOfAtheria().rubyGauntlet = !hideVisual;
			player.GetDamage(DamageClass.Generic) += .1f;
		}
	}
}