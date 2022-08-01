using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class SolarStorm : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a storm of Solar Flares\n" +
                "'Harness energy from the sun to vaporize your foes!'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 24;

            Item.damage = 150;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 8;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 6f;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = Item.sellPrice(0, 3, 25);
            Item.shoot = ItemID.PurificationPowder;
            Item.useAmmo = AmmoID.Flare;

            Item.holdStyle = ItemHoldStyleID.HoldFront;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4, 0);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            if (type == ProjectileID.Flare)
                type = ModContent.ProjectileType<SolarFlare>();
        }
    }
}