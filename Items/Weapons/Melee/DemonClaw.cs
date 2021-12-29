using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class DemonClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Damage increases throughout progression\n" +
				"'Come now! Let's play!'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 28;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 16;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
			Item.expert = true;
		}
	}
}