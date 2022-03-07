using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Cataracnia : ModItem
	{
        public override void SetStaticDefaults()
        {

			DisplayName.SetDefault("Cataracnia"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The blade wielded by the All Seer.");
		}

		public override void SetDefaults() 
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 76;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 21;
			Item.expert = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Ichor, 60);
			player.AddBuff(BuffID.Shine, 3600);
			player.AddBuff(BuffID.NightOwl, 3600);
			player.AddBuff(BuffID.Spelunker, 3600);
			player.AddBuff(BuffID.Hunter, 3600);
			player.AddBuff(BuffID.Dangersense, 3600);
		}
	}
}