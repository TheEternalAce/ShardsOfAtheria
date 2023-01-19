using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Tools;
using ShardsOfAtheria.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ShardsOfAtheria.Utilities.Entry;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class SoulExtractingDagger : ModItem
    {
        public int selectedSoul = 0;
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
            SacrificeTotal = 1;
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
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
            if (slayer.soulCrystals.Count == 0)
                CombatText.NewText(player.getRect(), Color.Blue, "You have no Soul Crystals to extract");
            else SelectSoul(slayer);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
            var selected = new TooltipLine(Mod, "SelectedSoul", "Selected: None");

            // Selected Soul Crystal
            for (int i = 0; i < entries.Count; i++)
            {
                if (selectedSoul < entries.Count)
                {
                    PageEntry entry = entries[selectedSoul];
                    if (slayer.soulCrystals.Contains(entry.crystalItem))
                    {
                        selected = new TooltipLine(Mod, "SelectedSoul", "Selected: " + entry.entryName)
                        {
                            OverrideColor = entry.entryColor
                        };
                    }
                }
            }
            tooltips.Add(selected);

            //Available Souls
            for (int i = 0; i < entries.Count; i++)
            {
                PageEntry entry = entries[i];
                if (slayer.soulCrystals.Contains(entry.crystalItem))
                {
                    tooltips.Add(new TooltipLine(Mod, "PageList", $"{entry.entryName} ({entry.mod})")
                    {
                        OverrideColor = entry.entryColor
                    });
                }
            }
            base.ModifyTooltips(tooltips);
        }

        public override bool CanUseItem(Player player)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
            return !player.immune && slayer.soulCrystals.Count > 0;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
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
            Projectile.NewProjectile(source, spawnLocation, Vector2.Zero, type, damage, 0, Main.myPlayer, 0, selectedSoul);
            return false;
        }

        public void SelectSoul(SlayerPlayer slayer)
        {
            if (++selectedSoul >= entries.Count)
            {
                selectedSoul = 0;
            }
            PageEntry entry = entries[selectedSoul];
            if (!slayer.soulCrystals.Contains(entry.crystalItem))
            {
                selectedSoul++;
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (selectedSoul == 0 && entries.Count > 0)
            {
                selectedSoul++;
            }
            else if (selectedSoul >= entries.Count)
            {
                selectedSoul = 0;
            }
            if (!player.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(entries[selectedSoul].crystalItem))
            {
                selectedSoul++;
            }
        }
    }
}
