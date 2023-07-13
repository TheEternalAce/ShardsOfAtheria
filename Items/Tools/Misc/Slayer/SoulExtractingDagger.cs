using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Tools;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ShardsOfAtheria.Utilities.Entry;

namespace ShardsOfAtheria.Items.Tools.Misc.Slayer
{
    public class SoulExtractingDagger : ModItem
    {
        public int soulIndex = 0;
        /* NOTE:
         * King Slime
         * Eye of Cthulhu
         * Brain of Cthulhu
         * Eater of Worlds
         * Queen Bee
         * Skeletron
         * Nova Stellar
         * Deerclops
         * Wall of Flesh
         * Queen Slime
         * Destroyer
         * Skeletron Prime
         * The Twins
         * Plantera
         * Golem
         * Duke Fishron
         * Empress of Light
         * Lunatic Cultist
         * Moon Lord
         * Senterra
         * Genesis
         * Death
         */

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 40;
            Item.rare = ItemRarityID.Yellow;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<ExtractingSoul>();
            Item.shootSpeed = 16f;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ShardsRecipes.EvilBar, 15)
                .AddIngredient(ItemID.Stinger, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            SlayerPlayer slayer = Main.LocalPlayer.Slayer();
            if (slayer.soulCrystalNames.Count == 0)
                CombatText.NewText(player.getRect(), Color.Blue, "You have no Soul Crystals to extract");
            else SelectSoul(slayer);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.Slayer();
            var selected = new TooltipLine(Mod, "SelectedSoul", "Selected: None");

            // Selected Soul Crystal
            for (int i = 0; i < entries.Count; i++)
            {
                if (soulIndex < entries.Count)
                {
                    PageEntry entry = entries[soulIndex];
                    string item = entry.crystalItem;
                    if (slayer.soulCrystalNames.Contains(item))
                    {
                        selected = new TooltipLine(Mod, "SelectedSoul", "Selected: " + entry.entryName)
                        {
                            OverrideColor = entry.entryColor
                        };
                    }
                }
            }
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), selected);

            //Available Souls
            for (int i = 0; i < entries.Count; i++)
            {
                PageEntry entry = entries[i];
                if (slayer.soulCrystalNames.Contains(entry.crystalItem))
                {
                    var line = new TooltipLine(Mod, "PageList", $"{entry.entryName} ({entry.mod})")
                    {
                        OverrideColor = entry.entryColor
                    };
                    tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
                }
            }
            base.ModifyTooltips(tooltips);
        }

        public override bool CanUseItem(Player player)
        {
            SlayerPlayer slayer = Main.LocalPlayer.Slayer();
            return !player.immune && slayer.soulCrystalNames.Count > 0;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoA.Log("Soul index to extract: ", soulIndex);
            SoA.Log("Old Soul Crystal list: ", player.Slayer().soulCrystalNames);
            Vector2 spawnLocation;
            if (player.direction == 1)
            {
                spawnLocation = player.Center + new Vector2(48, 0);
            }
            else
            {
                spawnLocation = player.Center + new Vector2(-48, 0);
            }
            damage = 50;
            if (Main.expertMode)
            {
                damage = 25;
            }
            else if (Main.masterMode)
            {
                damage = 13;
            }
            Projectile.NewProjectile(source, spawnLocation, Vector2.Zero, type, damage, 0, Main.myPlayer, 0, soulIndex);
            return false;
        }

        public void SelectSoul(SlayerPlayer slayer)
        {
            if (++soulIndex >= entries.Count)
            {
                soulIndex = 0;
            }
            PageEntry entry = entries[soulIndex];
            if (!slayer.soulCrystalNames.Contains(entry.crystalItem))
            {
                soulIndex++;
            }
        }

        public override void UpdateInventory(Player player)
        {
            Update(player);
        }
        public override void HoldItem(Player player)
        {
            Update(player);
        }

        public void Update(Player player)
        {
            if (soulIndex < 0)
            {
                soulIndex++;
            }
            else if (soulIndex >= entries.Count)
            {
                soulIndex = 0;
            }
            var slayer = player.Slayer();
            var crystalName = entries[soulIndex].crystalItem;
            if (!slayer.soulCrystalNames.Contains(crystalName))
            {
                SelectSoul(slayer);
            }
        }
    }
}
