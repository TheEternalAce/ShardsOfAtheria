using Terraria.ID;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaScythe : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Scythe");
            Tooltip.SetDefault("'The scythe of a godly machine'\n" +
              "'The new Death awaits'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.melee = true;
            item.knockBack = 6f;
            item.crit = 100;
            item.useTime = 20;
            item.useAnimation = 20;
            item.rare = ItemRarityID.Red;

            item.autoReuse = true;
            item.useTurn = true;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.width = 62;
            item.height = 68;
        }
    }
}