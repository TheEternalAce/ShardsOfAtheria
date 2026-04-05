using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class HandCanon : ChargeWeapon
    {
        public override DustInfo ChargeDustInfo => new(DustID.Torch);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(4);
            Item.AddElement(0);
            Item.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;
            Item.master = true;

            Item.damage = 65;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;
            Item.crit = 5;

            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Master;
            Item.value = 22500;
            Item.shoot = ModContent.ProjectileType<PlasmaShot>();
        }

        public override bool CanUseItem(Player player)
        {
            if (Charge == MaxCharge)
            {
                Item.useTime = 10;
                Item.useAnimation = 30;
                Item.reuseDelay = 6;
            }
            else
            {
                Item.useTime = Item.useAnimation = 28;
                Item.reuseDelay = 0;
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (Charge >= MaxCharge) ScreenShake.ShakeScreen(6, 60);
            SoundEngine.PlaySound(Item.UseSound, player.Center);
            return base.UseItem(player);
        }
    }
}