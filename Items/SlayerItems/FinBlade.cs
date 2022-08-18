using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class FinBlade : SlayerItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("'Getting hit by a fish has got to be humiliating'");

			base.SetStaticDefaults();
		}

		public override void SetDefaults() 
		{
			Item.width = 52;
			Item.height = 58;

			Item.damage = 78;
			Item.DamageType = DamageClass.Melee;
			Item.crit = 6;
			Item.knockBack = 6;

			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			Item.rare = ModContent.RarityType<SlayerRarity>();
			Item.value = Item.sellPrice(0, 3, 50);
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Main.NewText(Lang.GetNPCName(target.type) + " was hit by " + player.name + "'s fish");
		}
    }
}