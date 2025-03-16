using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class TwistedUtensil : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(2, 9);
            Item.AddElement(1);
            Item.AddRedemptionElement(3);
            Item.AddRedemptionElement(13);
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 26;
            Item.accessory = true;

            Item.damage = 42;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.crit = 2;

            Item.shoot = ModContent.ProjectileType<HomingTear>();
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().spoonBender = true;
        }
    }
}