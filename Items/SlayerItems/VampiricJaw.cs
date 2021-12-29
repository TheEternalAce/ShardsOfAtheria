using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	[AutoloadEquip(EquipType.Head)]
	public class VampiricJaw : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants immunity to Bleeding, Slow and Cursed\n" +
				"True melee attacks life steal");
			ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(gold: 15);
			Item.defense = 9;
			Item.rare = ItemRarityID.Expert;
		}

        public override void UpdateEquip(Player player)
		{
			player.buffImmune[BuffID.Bleeding] = true;
			player.buffImmune[BuffID.Slow] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.GetModPlayer<SMPlayer>().vampiricJaw = true;
		}
	}
}