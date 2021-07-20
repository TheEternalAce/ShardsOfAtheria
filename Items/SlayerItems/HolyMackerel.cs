using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;

namespace SagesMania.Items.SlayerItems
{
	public class HolyMackerel : SlayerItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("'Getting hit by a fish has got to be humiliating'");
		}

		public override void SetDefaults() 
		{
			item.damage = 78;
			item.melee = true;
			item.width = 52;
			item.height = 58;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = Item.sellPrice(gold: 50);
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.crit = 6;
			item.shootSpeed = 3.5f;
			item.autoReuse = true;
			item.rare = ItemRarityID.Expert;
		}

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
			Main.NewText(target.name + " was hit by " + player.name + "'s fish");
        }
    }
}