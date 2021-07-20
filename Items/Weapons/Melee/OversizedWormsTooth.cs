using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Melee
{
	public class OversizedWormsTooth : ModItem
	{
        public override void SetStaticDefaults()
        {

			DisplayName.SetDefault("Oversized Worm's Tooth"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Torn from an oversized worm.");
		}

		public override void SetDefaults() 
		{
			item.damage = 33;
			item.melee = true;
			item.width = 42;
			item.height = 48;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
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
			target.AddBuff(BuffID.Ichor, 600);
            target.AddBuff(BuffID.Weak, 600);
			target.AddBuff(BuffID.Chilled, 600);
		}
	}
}