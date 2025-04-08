using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ReactorMeltdown : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(5, 10);
            Item.AddElement(2);
            Item.AddElement(3);
            Item.AddRedemptionElement(7);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;

            Item.damage = 162;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2;
            Item.crit = 7;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemDefaults.RarityMartians;
            Item.value = Item.sellPrice(0, 2, 20);
            Item.shoot = ModContent.ProjectileType<ReactorMeltdownProj>();
        }
    }
}