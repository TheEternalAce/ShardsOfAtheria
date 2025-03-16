using ShardsOfAtheria.Projectiles.Magic.ThorSpear;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class ZEarthmoverSpear : EarthmoverSpear
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(3, 5);
            Item.AddElement(1);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(5);
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 130;

            Item.useTime = 24;
            Item.useAnimation = 24;

            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<ZEarthJavelin>();
        }

        public override void AddRecipes()
        {
        }
    }
}