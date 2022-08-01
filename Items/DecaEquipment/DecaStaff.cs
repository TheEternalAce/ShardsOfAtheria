using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaStaff : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The staff of a godly machine'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6f;
            Item.mana = 5;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item75;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.staff[Item.type] = true;

            Item.shootSpeed = 13f;
            Item.shoot = ModContent.ProjectileType<IonBeam>();
            base.SetDefaults();
        }
    }
}