using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Abaddon : VirtuousItem
    {
        public override int RequiredVirtue => CardinalSoulID.Temperance;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(7);
            Item.AddElement(2);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 44;
            Item.height = 26;

            Item.damage = 150;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7f;
            Item.crit = 5;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shootSpeed = 16f;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Dart;
        }
    }
}