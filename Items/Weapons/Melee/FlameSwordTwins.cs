using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Melee.FlameSwords;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class FlameSwordTwins : ChargeWeapon
    {
        int itemCombo = 0;

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
            Item.width = 56;
            Item.height = 68;
            Item.scale = 1.75f;

            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 6;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 1, 50);
            Item.shoot = ModContent.ProjectileType<FlameSword>();

            SoA.TryDungeonCall("addFinesseWeapon", Type);
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.SilverBar, 15)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.TungstenBar, 15)
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
            Item.useStyle = ItemUseStyleID.Swing;
            if (itemCombo == 2 || Charge == 300)
            {
                Item.useStyle = ItemUseStyleID.Rapier;
                type++;
                velocity *= 0.6f;
                damage = (int)(damage * 1.5f);
            }
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (++itemCombo > 2) itemCombo = 0;
            if (FullyCharged)
            {
                itemCombo = 0;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 1);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}