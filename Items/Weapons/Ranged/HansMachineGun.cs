using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class HansMachineGun : ZChargeable
    {
        public override int ZChargedItem => ModContent.ItemType<ZHansMachineGun>();

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(4, 5);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
            Item.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Item.width = 198;
            Item.height = 76;
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 3, 16f);

            Item.damage = 60;
            Item.knockBack = 4f;

            Item.useAnimation = 300;
            Item.reuseDelay = 60;
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = true;
            Item.consumeAmmoOnFirstShotOnly = true;

            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = Item.sellPrice(0, 4, 25);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-50, -20);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet) type = ModContent.ProjectileType<HansBullet>();
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            float rotation = velocity.ToRotation();
            Vector2 muzzleOffset = new Vector2(45, -20 * player.direction).RotatedBy(rotation);
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) position += muzzleOffset;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(Item.UseSound, player.Center);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}