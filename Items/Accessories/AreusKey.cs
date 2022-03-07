using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
	public class AreusKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Unlocks your true potential\n" +
				"'Now, nothing but your own competence holds you back.'");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 22;
			Item.value = Item.buyPrice(gold: 25);
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SoAPlayer>().areusKey = true;
			player.GetDamage(DamageClass.Generic) += .5f;
			player.statLifeMax2 *= (int)1.5f;
			player.moveSpeed += .25f;
			player.statDefense *= (int)1.5f;
			player.statManaMax2 *= (int)1.5f;
		}
	}
}