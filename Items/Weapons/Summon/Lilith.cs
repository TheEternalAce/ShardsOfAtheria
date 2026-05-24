using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Summon.Active;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class Lilith : SinfulItem
    {
        public override int RequiredSin => SinnerPlayer.Lust;

        // Base damages: 30, 100, 170
        public override int[] DamageSpread => [0, 70, 70];

        public override void SetStaticDefaults()
        {
            Item.AddElement(3);
            Item.AddDamageType(6, 11);
            Item.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.DefaultToWhip(ModContent.ProjectileType<LilithWhip>(), 30, 2, 3.45f, 26);
            Item.autoReuse = true;
            Item.ArmorPenetration = 10;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2f;
            float rotation = MathHelper.ToRadians(15f);
            float spawnDistance = 100f;
            Vector2 normalVelocity = Vector2.Normalize(velocity);
            int type2 = ModContent.ProjectileType<LilithHeart>();
            int damage2 = (int)(damage * 0.5f);

            for (float i = 0; i < numberProjectiles; i++)
            {
                Vector2 positionOffset = -normalVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * spawnDistance;
                Projectile.NewProjectile(source, position + positionOffset, normalVelocity * 16f, type2, damage2, knockback);
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.PiOver4) * (1 - Main.rand.NextFloat(0.2f));
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback);
            }
            return false;
        }
    }
}