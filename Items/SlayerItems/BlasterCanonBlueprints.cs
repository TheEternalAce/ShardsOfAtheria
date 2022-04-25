using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class BlasterCanonBlueprints : SlayerItem
	{
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 22;
			Item.maxStack = 999;
			Item.UseSound = new LegacySoundStyle(SoundID.MenuTick, 0);
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ProjectileID.None;
			Item.consumable = true;
		}

        public override bool? UseItem(Player player)
        {
			player.GetModPlayer<SlayerPlayer>().blueprintRead = true;
			return true;
        }
    }
}
