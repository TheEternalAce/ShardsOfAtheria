using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok;
using ShardsOfAtheria.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    [AutoloadEquip(EquipType.Shield)]
    public class GenesisAndRagnarok : ModItem
    {
        public int combo = 0;

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Ice.Add(Type);
            WeaponElements.Fire.Add(Type);
            WeaponElements.Electric.Add(Type);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ShardsPlayer shardsPlayer = Main.LocalPlayer.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;
            if (upgrades == 0)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GenesisAndRagnarokBase")));
            }
            if (upgrades == 1)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GenesisAndRagnarokUpgrade1")));
            }
            if (upgrades == 2)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GenesisAndRagnarokUpgrade2")));
            }
            if (upgrades == 3)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GenesisAndRagnarokUpgrade3")));
            }
            if (upgrades == 4)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GenesisAndRagnarokUpgrade4")));
            }
            if (upgrades == 5)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GenesisAndRagnarokUpgrade5")));
            }
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 3f;
            Item.crit = 6;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 4);
            combo = 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 18)
                .AddIngredient(ModContent.ItemType<ChargedFeather>(), 7)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
                .AddIngredient(ModContent.ItemType<GenesisAndRagnarok>())
                .AddIngredient(ModContent.ItemType<MemoryFragment>())
                .Register();

            CreateRecipe()
                .AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
                .AddIngredient(ModContent.ItemType<GenesisAndRagnarok>())
                .AddIngredient(ModContent.ItemType<MemoryFragment>())
                .AddIngredient(ItemID.ChlorophyteBar, 14)
                .Register();

            CreateRecipe()
                .AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
                .AddIngredient(ModContent.ItemType<GenesisAndRagnarok>())
                .AddIngredient(ModContent.ItemType<MemoryFragment>())
                .AddIngredient(ItemID.BeetleHusk, 16)
                .Register();

            CreateRecipe()
                .AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
                .AddIngredient(ModContent.ItemType<GenesisAndRagnarok>())
                .AddIngredient(ModContent.ItemType<MemoryFragment>())
                .AddIngredient(ItemID.FragmentSolar, 18)
                .Register();

            CreateRecipe()
                .AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
                .AddIngredient(ModContent.ItemType<GenesisAndRagnarok>())
                .AddIngredient(ModContent.ItemType<MemoryFragment>())
                .AddIngredient(ItemID.LunarBar, 20)
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            ShardsPlayer shardsPlayer = Main.LocalPlayer.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            if (player.altFunctionUse == 2)
            {
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
                }
                else
                {
                    switch (combo)
                    {
                        case 0:
                            Item.shoot = ModContent.ProjectileType<Genesis_Spear>();
                            Item.shootSpeed = 3.7f;
                            Item.UseSound = SoundID.Item1;
                            break;
                        case 1:
                            Item.shoot = ModContent.ProjectileType<Genesis_Whip>();
                            Item.shootSpeed = 24f;
                            Item.UseSound = SoundID.Item116;
                            break;
                        case 2:
                            if (upgrades >= 1)
                            {
                                Item.shoot = ModContent.ProjectileType<Genesis_Spear2>();
                                Item.shootSpeed = 30f;
                                Item.UseSound = SoundID.Item71;
                            }
                            break;
                        case 3:
                        case 4:
                            if (upgrades >= 4)
                            {
                                Item.shoot = ModContent.ProjectileType<Genesis_Sword>();
                                Item.shootSpeed = 1f;
                                Item.UseSound = SoundID.DD2_MonkStaffSwing;
                            }
                            break;
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
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            int comboExtra = upgrades >= 1 ? 1 : 0;
            int comboExtra2 = upgrades >= 4 ? 2 : 0;
            if (combo >= 1 + comboExtra + comboExtra2)
                combo = 0;
            else combo++;
            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            if (upgrades == 1)
                damage += .5f;
            if (upgrades == 2)
                damage += 1f;
            if (upgrades == 3)
                damage += 2f;
            if (upgrades == 4)
                damage += 3f;
            if (upgrades == 5)
                damage += 5.4f;
        }

        public override void HoldItem(Player player)
        {
            player.ShardsOfAtheria().showRagnarok = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<Genesis_Whip>())
            {
                float ai4 = (Main.rand.NextFloat() - 0.5f) * ((float)Math.PI / 4f);
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, ai4);
                return false;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] > 0)
                return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}