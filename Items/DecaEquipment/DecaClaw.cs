using Terraria.ID;

namespace SagesMania.Items.DecaEquipment
{
    class DecaClaw : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Claws");
            Tooltip.SetDefault("'Claws of a Godly machine'\n" +
                "'Even the jungle tyrant fears his wrath'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.melee = true;
            item.knockBack = 2f;
            item.crit = 100;
            item.useTime = 1;
            item.useAnimation = 10;
            item.rare = ItemRarityID.Red;

            item.autoReuse = true;
            item.useTurn = true;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.width = 40;
            item.height = 40;
            item.scale = .85f;
        }
    }
}
