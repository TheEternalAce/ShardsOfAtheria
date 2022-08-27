using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class StrangeTissueSample : SlayerItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Summons a pet Creeper to follow you");

			base.SetStaticDefaults();
		}
        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(0,  7, silver: 50);
			Item.rare = ModContent.RarityType<SlayerRarity>();
			Item.shoot = ModContent.ProjectileType<PetCreeper>();
			Item.buffType = ModContent.BuffType<CreeperPetBuff>();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600);
			}
		}
	}
}