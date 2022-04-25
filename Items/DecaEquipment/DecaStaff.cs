using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaStaff : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Staff");
            Tooltip.SetDefault("'The staff of a godly machine'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 100;
            Item.useTime = 6;
            Item.useAnimation = 6;

            Item.shoot = ModContent.ProjectileType<IonBeam>();
            Item.shootSpeed = 13f;
            Item.mana = 5;

            Item.noMelee = true;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item75;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 50;
            Item.height = 50;
        }
    }
}