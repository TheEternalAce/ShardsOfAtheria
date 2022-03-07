using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Biochemical;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
	public class Parasite : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("12% increased biochemical damage\n" +
				"Slowly drains life");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
			Item.defense = 15;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(ModContent.GetInstance<BiochemicalDamage>()) += .12f;
			player.GetModPlayer<ParasitePlayer>().parasite = true;
		}
	}

	public class ParasitePlayer : ModPlayer
    {
		public bool parasite;

        public override void ResetEffects()
        {
			parasite = false;
        }

        public override void UpdateBadLifeRegen()
		{
			if (parasite)
			{
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes .5 life lost per second.
                Player.lifeRegen -= 1;
			}
		}
    }
}