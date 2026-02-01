using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Projectiles.Ranged.PlagueRail;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class PlagueHandgun : ChargeWeapon
    {
        public override DustInfo ChargeDustInfo => new(ModContent.DustType<PlagueDust>(), 100);

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(0, 6, 8);
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 28;
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 24, 16f);

            Item.damage = 15;
            Item.knockBack = 2f;

            Item.UseSound = SoundID.Item41;
            Item.autoReuse = true;

            Item.rare = ItemDefaults.RarityMechs;
            Item.value = Item.sellPrice(0, 4, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BionicBarItem>(15)
                .AddIngredient<PlagueCell>(20)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            Item plagueRail = ModContent.GetInstance<PlagueRailgun>().Item;
            return !player.HasAmmo(plagueRail) && !player.Overdrive();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                Item plagueRail = ModContent.GetInstance<PlagueRailgun>().Item;
                if (player.HasAmmo(plagueRail))
                {
                    bool dontConsumeCell = !player.ItemAnimationJustStarted || Main.rand.NextFloat() < 0.66f;
                    player.PickAmmo(plagueRail, out int _, out float _, out int _, out float _, out int _, dontConsumeCell);
                    type = ModContent.ProjectileType<PlagueBullet>();
                    velocity.Normalize();
                    velocity *= 16;
                }
            }
            if (Item.useAnimation > Item.useTime) velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5f));
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
            base.ModifyWeaponDamage(player, ref damage);
        }

        public override bool CanUseItem(Player player)
        {
            if (FullyCharged)
            {
                Item.useTime = 12;
                Item.useAnimation = 72;
                Item.reuseDelay = 6;
            }
            else
            {
                Item.useTime = Item.useAnimation = 24;
                Item.reuseDelay = 0;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.itemAnimation != player.itemAnimationMax) SoundEngine.PlaySound(Item.UseSound, player.Center);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0);
        }
    }
}
