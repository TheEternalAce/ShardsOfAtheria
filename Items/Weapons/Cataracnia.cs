using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
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
			item.damage = 36;
			item.melee = true;
			item.width = 64;
			item.height = 76;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.crit = 21;
			item.expert = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(BuffID.Ichor, 60);
            target.AddBuff(BuffID.Blackout, 3600);
			player.AddBuff(BuffID.Shine, 18000);
			player.AddBuff(BuffID.NightOwl, 18000);
			player.AddBuff(BuffID.Spelunker, 18000);
			player.AddBuff(BuffID.Hunter, 18000);
			player.AddBuff(BuffID.Dangersense, 18000);
		}
	}
}