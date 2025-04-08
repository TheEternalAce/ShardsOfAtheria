using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Abaddon : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(7);
            Item.AddElement(2);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 26;
            Item.master = true;

            Item.damage = 150;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7f;
            Item.crit = 5;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.noMelee = true;

            Item.shootSpeed = 10f;
            Item.rare = ItemDefaults.RaritySinful;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Dart;
        }
    }
}