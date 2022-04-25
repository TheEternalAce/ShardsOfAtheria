using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.Potions
{
	public class BoneMarrowInjection : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Strengthens your bones");
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.consumable = true;
			Item.useTurn = true;
			Item.buffType = ModContent.BuffType<BoneStrength>();
			Item.buffTime = (4 * 60) * 60;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<EmptyNeedle>())
				.AddIngredient(ItemID.Bone)
				.AddTile(TileID.AlchemyTable)
				.Register();
		}

		public override void OnConsumeItem(Player player)
		{
			player.QuickSpawnItem(player.GetSource_FromThis(), ModContent.ItemType<EmptyNeedle>());
			player.AddBuff(ModContent.BuffType<InjectionShock>(), 5 * 60);
		}

		public override bool CanUseItem(Player player)
		{
			if (!player.HasBuff(ModContent.BuffType<InjectionShock>()))
				return true;
			else return false;
		}
	}
}