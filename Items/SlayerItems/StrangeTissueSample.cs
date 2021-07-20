using SagesMania.Buffs;
using SagesMania.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.SlayerItems
{
	public class StrangeTissueSample : SlayerItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Summons a pet Creeper to follow you");
        }
        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 7, silver: 50);
			item.rare = ItemRarityID.Expert;
			item.shoot = ModContent.ProjectileType<PetCreeper>();
			item.buffType = ModContent.BuffType<PetCreeperBuff>();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}