using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using System.Collections.Generic;
using Terraria;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    class DecaClaw : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Claws");
            Tooltip.SetDefault("'Claws of a Godly machine'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2f;
            Item.crit = 100;
            Item.useTime = 1;
            Item.useAnimation = 10;

            Item.autoReuse = false;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 40;
            Item.height = 40;
            Item.scale = .85f;

            Item.shoot = ModContent.ProjectileType<DecaClawProj>();
            Item.shootSpeed = 2.1f;
        }
    }
}
