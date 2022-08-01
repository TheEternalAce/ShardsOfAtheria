using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.GameContent.Creative;
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

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 22;
			Item.accessory = true;

			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.buyPrice(0, 10);
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