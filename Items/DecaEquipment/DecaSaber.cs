using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaSaber : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Saber");
            Tooltip.SetDefault("'The blade of a godly machine'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 100;
            Item.useTime = 20;
            Item.useAnimation = 20;

            Item.autoReuse = true;
            Item.useTurn = false;
            Item.UseSound = SoundID.Item71;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 74;
            Item.height = 74;

            Item.shoot = ModContent.ProjectileType<IonCutter>();
            Item.shootSpeed = 13f;
        }
    }
}