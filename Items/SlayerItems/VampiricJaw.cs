using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.SlayerItems
{
	[AutoloadEquip(EquipType.Head)]
	public class VampiricJaw : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants immunity to Bleeding, Slow and Cursed\n" +
				"True melee attacks life steal");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 15);
			item.defense = 9;
			item.rare = ItemRarityID.Expert;
		}

        public override void UpdateEquip(Player player)
		{
			player.buffImmune[BuffID.Bleeding] = true;
			player.buffImmune[BuffID.Slow] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.GetModPlayer<SMPlayer>().vampiricJaw = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}
	}
}