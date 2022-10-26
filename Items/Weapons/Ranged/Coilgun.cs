using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Items.Weapons.Ammo;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Globals;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Coilgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Tears through enemy armor\n" +
                "'Uses electromagnetic coils to fire projectiles at insane velocities'\n" +
                "'Areus Railgun's older brother'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 26;

            Item.damage = 150;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 5;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item38;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2, 75);
            Item.shoot = ItemID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.ArmorPenetration = 20;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}