using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusBow : OverchargeWeapon
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 54;

            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 5);
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
            chargeAmount = 0.167f;
            overchargeShoot = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 11)
                .AddRecipeGroup(ShardsRecipes.Gold, 3)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 7)
                .AddIngredient(ModContent.ItemType<ChargedFeather>(), 10)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<AreusArrowProj>();
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<OverchargePlayer>().overcharged)
            {
                Item.useTime = 5;
                Item.useAnimation = 25;
                Item.reuseDelay = 10;
            }
            else
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.reuseDelay = 0;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            OverchargePlayer overchargePlayer = player.GetModPlayer<OverchargePlayer>();
            if (overchargePlayer.overcharged)
            {
                Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                proj.GetGlobalProjectile<OverchargedProjectile>().overcharged = true;
                if (player.itemAnimation == 5)
                {
                    overchargePlayer.overcharge = 0f;
                }
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void Overcharge(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 1)
        {
            ConsumeOvercharge(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}