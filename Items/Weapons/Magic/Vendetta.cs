using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class Vendetta : SinfulItem
    {
        public override int RequiredSin => SinnerPlayer.ENVY;

        public override int[] DamageSpread => [0, 30, 40];

        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 64;
            Item.height = 62;

            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3.75f;
            Item.mana = 6;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<VendettaBeam>();
        }
    }
}