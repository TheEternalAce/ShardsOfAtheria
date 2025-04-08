using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Projectiles.Magic.ShockTome;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class DataTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

            Item.AddDamageType(5, 11);
            Item.AddElement(2);
            Item.AddElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 56;

            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.mana = 12;

            Item.useTime = 5;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 10f;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<LightningBoltFriendly>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.SoulofFright, 15)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.PlatinumBar, 10)
                .AddIngredient(ItemID.SoulofFright, 15)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
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
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(50, 50);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Electric, 0, 0, 100, default, 0.6f);
                        dust.velocity = player.velocity;
                        if (Main.rand.NextBool(3)) dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                        dust.noGravity = true;
                    }
                }
                if (charge == MaxCharge - 1) SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
            }
            if (player.HeldItem == Item && player.ItemAnimationActive && player.ItemAnimationEndingOrEnded) charge = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<VineWhip>();
                Item.useTime = 34;
                Item.useAnimation = 34;
                Item.reuseDelay = 0;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<LightningBoltFriendly>();
                Item.useTime = 5;
                Item.useAnimation = 25;
                Item.reuseDelay = 15;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationEndingOrEnded) charge = 0;
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<VineWhip>())
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(25);
                if (charge == MaxCharge)
                {
                    numberProjectiles = 12;
                    rotation = MathHelper.TwoPi;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedByRandom(rotation) * Main.rand.NextFloat(0.66f, 1f);
                        Projectile.NewProjectile(source, Main.MouseWorld, perturbedSpeed, type - 1, damage, knockback);
                    }
                    return false;
                }
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(rotation) * Main.rand.NextFloat(0.66f, 1f);
                    Projectile.NewProjectile(source, position, perturbedSpeed, type, damage / 2, knockback);
                }
            }
            else if (charge == MaxCharge)
            {
                ShardsHelpers.CallStorm(source, Main.MouseWorld, 2, damage, knockback, Item.DamageType, 0f, 3f);
                return false;
            }
            return true;
        }

        public override void UseItemFrame(Player player)
        {
            base.UseItemFrame(player);
        }
    }
}