using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
	public class UnanalyzedMicrobe: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("'Who knows what it is?'");
		}

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 10;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.rare = ItemRarityID.Gray;
		}
    }
}