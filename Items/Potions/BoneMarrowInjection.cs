using ShardsOfAtheria.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Potions
{
	public class BoneMarrowInjection : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 30;
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.maxStack = 9999;

			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
			Item.useTurn = true;

			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.Orange;

			Item.buffType = ModContent.BuffType<BoneStrength>();
			Item.buffTime = 14400;
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
			player.AddBuff(ModContent.BuffType<InjectionShock>(), 300);
		}

		public override bool CanUseItem(Player player)
		{
			if (!player.HasBuff(ModContent.BuffType<InjectionShock>()))
				return true;
			else return false;
		}
	}

	public class BoneStrength : ModBuff
	{
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += .1f;
			player.endurance += .2f;
		}
	}
}