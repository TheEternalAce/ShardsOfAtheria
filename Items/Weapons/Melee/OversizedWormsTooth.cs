using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
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
			Item.damage = 33;
			Item.DamageType = DamageClass.Melee;
			Item.width = 42;
			Item.height = 48;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0,  10);
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 5;
			Item.expert = true;
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