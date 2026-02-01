using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Melee.FlameBuster;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class FlameKnuckleBuster : ChargeWeapon
    {
        public override DustInfo ChargeDustInfo => new(DustID.Torch, 100);
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(3, 10);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(8);
        }

        public override void SetDefaults()
        {
            smallChargeRing = true;
            Item.width = 34;
            Item.height = 34;

            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 6;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 1, 50);
            Item.shoot = ModContent.ProjectileType<FlameBuster>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 15)
                .AddIngredient(ItemID.SilverBar, 7)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 15)
                .AddIngredient(ItemID.TungstenBar, 7)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
            base.ModifyWeaponDamage(player, ref damage);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (FullyCharged)
            {
                if (player.controlUp)
                {
                    velocity = Vector2.One;
                    velocity.X *= player.direction;
                    velocity *= Item.shootSpeed;
                    damage = (int)(damage * 1.5f);
                }
                else
                {
                    velocity.Normalize();
                    type = ModContent.ProjectileType<FlamethrowerBuster>();
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (FullyCharged)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 1);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}