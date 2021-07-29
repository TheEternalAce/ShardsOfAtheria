using Terraria;
using Terraria.ID;

namespace SagesMania.Items.SlayerItems
{
	public class DeathsScythe : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Death's Scythe");
			Tooltip.SetDefault("[c/323232:...]");
		}

		public override void SetDefaults()
		{
			item.damage = 200000;
			item.melee = true;
			item.width = 60;
			item.height = 56;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = Item.sellPrice(gold: 50);
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.useTurn = true;
			item.crit = 100;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.WitheredArmor, 600);
			target.AddBuff(BuffID.WitheredWeapon, 600);
			target.AddBuff(BuffID.Ichor, 600);
			target.AddBuff(BuffID.Bleeding, 600);
			target.AddBuff(BuffID.Daybreak, 600);
		}
	}
}