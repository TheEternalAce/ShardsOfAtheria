using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaClaw : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The claw of a Godly machine'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.scale = .85f;

            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2f;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 2.1f;
            Item.shoot = ModContent.ProjectileType<DecaClawProj>();
            base.SetDefaults();
        }
    }
}
