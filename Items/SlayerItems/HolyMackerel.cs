using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class HolyMackerel : SlayerItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("'Getting hit by a fish has got to be humiliating'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 78;
			Item.DamageType = DamageClass.Melee;
			Item.width = 52;
			Item.height = 58;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(gold: 50);
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 6;
			Item.shootSpeed = 3.5f;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Expert;
		}

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
			Main.NewText(target.name + " was hit by " + player.name + "'s fish");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Main.NewText(target.nameOver + " was hit by " + player.name + "'s fish");
		}
    }
}