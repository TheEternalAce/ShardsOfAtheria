using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaSaber : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The blade of a godly machine'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 74;

            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shootSpeed = 13f;

            Item.shoot = ModContent.ProjectileType<IonCutter>();
            base.SetDefaults();
        }
    }
}