using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Melee.FlameBuster;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class FlameKnuckleBuster : ModItem
    {
        int charge = 0;
        const int MaxCharge = 150;
        public override void SetStaticDefaults()
        {
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(8);
        }

        public override void SetDefaults()
        {
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.SilverBar, 15)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(damage);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (charge == MaxCharge)
            {
                if (player.Overdrive())
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

        public override void UpdateInventory(Player player)
        {
            if (!player.ItemAnimationActive || player.HeldItem != Item)
            {
                if (charge < MaxCharge)
                {
                    charge++;
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(25, 25);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Torch, 0, 0, 100);
                        dust.velocity = player.velocity;
                        if (Main.rand.NextBool(3)) dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                        dust.noGravity = true;
                    }
                }
                if (charge == MaxCharge - 1) SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int previousCharge = charge;
            charge = 0;
            if (previousCharge == MaxCharge)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 1);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}