using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Ranged.Hunter;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class EntropicHunter : ModItem
    {
        bool bombThrow = false;
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(7);
            Item.AddElement(3);
            Item.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 68;

            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemDefaults.RaritySlayer;
            Item.value = Item.sellPrice(0, 5);
            Item.shoot = ModContent.ProjectileType<HunterBomb>();
            Item.useAmmo = AmmoID.Arrow;
        }

        public override bool AltFunctionUse(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 6;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = SoundID.Item1;
                Item.noUseGraphic = true;
                Item.shootSpeed = 4f;
                Item.useAmmo = AmmoID.None;
                bombThrow = true;
            }
            else if (!bombThrow)
            {
                Item.useTime = 18;
                Item.useAnimation = 18;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = SoundID.Item5;
                Item.noUseGraphic = false;
                Item.shootSpeed = 16f;
                Item.useAmmo = AmmoID.Arrow;
            }
            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (bombThrow)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.66f, 1f);
                type = Item.shoot;
            }
            else if (NPC.downedGolemBoss && type == ProjectileID.WoodenArrowFriendly) type = ModContent.ProjectileType<EntropicArrow>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type != Item.shoot && player.ownedProjectileCounts[Item.shoot + 1] > 0)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.active && proj.owner == player.whoAmI && proj.type == Item.shoot + 1)
                    {
                        ShardsHelpers.DustRing(proj.Center, 20, DustID.ShadowbeamStaff);
                        Projectile.NewProjectile(source, proj.Center, proj.DirectionTo(Main.MouseWorld) * velocity.Length() * Main.rand.NextFloat(0.66f, 1f), type, (int)(damage * 0.25f), knockback);
                    }
                }
            }
            bombThrow = false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -4);
        }
    }
}