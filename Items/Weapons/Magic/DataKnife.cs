using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class DataKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(1);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
            Item.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;

            Item.damage = 18;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 2f;
            Item.mana = 8;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 12f;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<ShockKnife>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 15)
                .AddIngredient(ItemID.GoldBar, 7)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 15)
                .AddIngredient(ItemID.PlatinumBar, 7)
                .AddIngredient(ItemID.SoulofFright, 10)
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
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(25, 25);
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(25);
            for (int i = 0; i < numberProjectiles; i++)
            {
                float ai0 = 1f;
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                if (i == 0 || i == 3) perturbedSpeed *= 1.2f;
                if (i > 1) ai0 = -1f;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback,
                    player.whoAmI, ai0);
            }
            return false;
        }
    }
}