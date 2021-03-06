using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class Cataracnia : SlayerItem
	{
        public override void SetStaticDefaults()
        {

			DisplayName.SetDefault("Cataracnia"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("'The blade wielded by the All Seer'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.width = 36;
			Item.height = 54;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(0,  10);
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 4;
			Item.rare = ModContent.RarityType<SlayerRarity>();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Ichor, 120);
			player.AddBuff(BuffID.Shine, 7200);
			player.AddBuff(BuffID.NightOwl, 7200);
			player.AddBuff(BuffID.Spelunker, 7200);
			player.AddBuff(BuffID.Hunter, 7200);
			player.AddBuff(BuffID.Dangersense, 7200);
		}
	}
}