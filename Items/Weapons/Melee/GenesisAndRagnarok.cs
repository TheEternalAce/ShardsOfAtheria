using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class GenesisAndRagnarok : ModItem
    {
        public int combo = 0;
        public int comboTimer;
        public int level = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genesis and Ragnarök");
            Tooltip.SetDefault("Preforms a combo of a spear thrust, whip swing and spear throw\n" +
                "If an attack is not followed up after an attack, the combo will reset\n" +
                "Right click to bring up a shield, release right click to throw the shield\n" +
                "This shield grants 20 defense and is capable of parrying and reflecting projectiles");
        }

        public override void SetDefaults()
        {
            Item.damage = 300;
            Item.DamageType = DamageClass.Melee;
            Item.channel = false;
            Item.width = 94;
            Item.height = 104;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = false;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.channel = true;
            Item.crit = 6;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            combo = 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentSolar, 18)
                .AddIngredient(ItemID.FragmentVortex, 18)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void HoldItem(Player player)
        {
            int comboTimerMax = 60;
            // If the item is not being used, continue
            if (player.itemAnimation == 0)
            {
                // If combo is not the spear, decrement the timer
                if (combo > 0)
                    comboTimer--;

                // If the timer is 0, reset the timer and combo
                if (comboTimer == 0)
                {
                    comboTimer = comboTimerMax;
                    combo = 0;
                }
            }
            else comboTimer = comboTimerMax;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.damage = 200;
                Item.knockBack = 6;
                Item.UseSound = SoundID.Item15;
                Item.shoot = ModContent.ProjectileType<Ragnarok_Shield>();
                Item.shootSpeed = 0;
            }
            else
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] > 0)
                {
                    Item.shoot = ProjectileID.None;
                    Item.shootSpeed = 4.5f;
                    Item.UseSound = null;
                    Item.knockBack = 6f;
                }
                else
                {
                    if (combo == 0)
                    {
                        Item.shoot = ModContent.ProjectileType<Genesis_Spear>();
                        Item.shootSpeed = 4.5f;
                        Item.UseSound = SoundID.Item1;
                        Item.knockBack = 6f;
                    }
                    if (combo == 1)
                    {
                        Item.shoot = ModContent.ProjectileType<Genesis_Whip>();
                        Item.shootSpeed = 24f;
                        Item.UseSound = SoundID.Item116;
                        Item.knockBack = 6f;
                    }
                    if (combo == 2)
                    {
                        Item.shoot = ModContent.ProjectileType<Genesis_Spear2>();
                        Item.shootSpeed = 30f;
                        Item.UseSound = SoundID.Item71;
                        Item.knockBack = 3f;
                    }
                    if (combo == 3)
                    {
                        Item.shoot = ModContent.ProjectileType<Genesis_Sword>();
                        Item.shootSpeed = 30f;
                        Item.UseSound = SoundID.DD2_MonkStaffSwing;
                        Item.knockBack = 3f;
                    }
                }
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Spear>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Whip>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Spear2>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Sword>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (combo == 3)
                combo = 0;
            else combo++;
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<Genesis_Whip>())
            {
                float ai4 = (Main.rand.NextFloat() - 0.5f) * ((float)Math.PI / 4f);
                Projectile.NewProjectile(source, player.Center, Vector2.Normalize(Main.MouseWorld - player.Center), type, damage, knockback, player.whoAmI, 0f, ai4);
                return false;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] > 0)
                return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}