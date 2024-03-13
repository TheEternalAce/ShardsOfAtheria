using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SinfulSouls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Pantheon : SinfulItem
    {
        //public override void SetStaticDefaults()
        //{
        //    Item.AddElement(2);
        //    Item.AddRedemptionElement(7);
        //}

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 70;

            Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;
            Item.crit = 6;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemDefaults.RaritySinful;
            Item.value = Item.sellPrice(0, 5);
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -4);
        }
    }
}