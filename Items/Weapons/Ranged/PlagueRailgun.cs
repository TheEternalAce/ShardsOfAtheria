using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Tools.ToggleItems;
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
    public class PlagueRailgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(6, 8);
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 112;
            Item.height = 28;

            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7f;
            Item.crit = 6;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item38;
            Item.noMelee = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueEarlyHardmode;
            Item.shoot = ModContent.ProjectileType<PlagueBeam>();
            Item.shootSpeed = 1f;
            Item.useAmmo = ModContent.ItemType<PlagueCell>();
            Item.ArmorPenetration = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BionicBarItem>(20)
                .AddIngredient<PlagueCell>(30)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() < 0.66f;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (charge == MaxCharge) type = ModContent.ProjectileType<VolatilePlagueBeam>();
            if (type == ModContent.ProjectileType<PlagueBeam2>() || type == ModContent.ProjectileType<VolatilePlagueBeam>())
            {
                var cloneVelocity = velocity;
                cloneVelocity.Normalize();
                position += cloneVelocity * 66f;
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
            if (charge >= MaxCharge) damage += 1f;
        }

        int charge = 0;
        const int MaxCharge = 300;
        public override void UpdateInventory(Player player)
        {
            var cable = ToggleableTool.GetInstance<BrokenCable>(player);
            if ((!player.ItemAnimationActive || player.HeldItem != Item) && (cable is null || !cable.Active))
            {
                if (charge < MaxCharge)
                {
                    charge++;
                    if (SoA.ClientConfig.chargeSound && charge % 25 == 0) SoundEngine.PlaySound(SoA.ZeroCharge, player.Center);
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(50, 50);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, ModContent.DustType<PlagueDust>(), 0, 0, 100, default, 0.6f);
                        dust.velocity = player.velocity;
                        if (Main.rand.NextBool(3)) dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                        dust.noGravity = true;
                    }
                }
                if (charge == MaxCharge - 1) SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
            }
            if (player.HeldItem == Item && player.ItemAnimationActive && player.ItemAnimationEndingOrEnded) charge = 0;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (charge == MaxCharge)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 1);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 3);
        }

        public override void HoldItem(Player player)
        {
            player.scope = true;
        }
    }
}