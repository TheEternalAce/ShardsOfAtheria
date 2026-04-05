using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Projectiles.Magic.Gambit;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class HitscanGun : ModItem
    {
        private readonly int standardShot = ModContent.ProjectileType<HitscanBullet>();
        private readonly int bouncingShot = ModContent.ProjectileType<HitscanBullet_Bounce3>();

        int shotType;

        public override string Texture => ModContent.GetInstance<AreusMagnum>().Texture;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.AddDamageType(7);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 26;
            Item.DefaultToRangedWeapon(ModContent.ProjectileType<HitscanBullet>(), AmmoID.Bullet, 24, 1f, true);
            shotType = standardShot;

            Item.damage = 62;
            Item.knockBack = 2f;
            Item.crit = 0;

            Item.UseSound = SoundID.Item41;

            Item.rare = ItemDefaults.RarityDevSet;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void RightClick(Player player)
        {
            if (shotType == standardShot) shotType = bouncingShot;
            else shotType = standardShot;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = Item.shoot;
            if (type == ModContent.ProjectileType<ElecCoin>())
            {
                damage = 0;
                velocity = velocity.RotatedBy(MathHelper.ToRadians(-15) * player.direction) + player.velocity;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<ElecCoin>())
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool AltFunctionUse(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<ElecCoin>()] < 4;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.DefaultToRangedWeapon(ModContent.ProjectileType<ElecCoin>(), AmmoID.None, 20, 10f, true);
                Item.UseSound = SoA.Coin;
            }
            else
            {
                Item.DefaultToRangedWeapon(shotType, AmmoID.Bullet, 24, 1f, true);
                Item.UseSound = SoundID.Item41;
            }
            return base.CanUseItem(player);
        }
    }
}
