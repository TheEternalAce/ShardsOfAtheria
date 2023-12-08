using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static ShardsOfAtheria.Utilities.Entry;

namespace ShardsOfAtheria.Items.Tools.Misc.Slayer
{
    public class Necronomicon : ModItem
    {
        public int page;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 56;

            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;

            Item.rare = ItemDefaults.RaritySlayer;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.life > 0 && npc.boss)
                    return false;
            }
            return page == 1;
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
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt) && page > 0)
            {
                page = 0;
            }
            else if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                if (page > 0)
                {
                    page--;
                }
                else
                {
                    page = SoA.MaxNecronomiconPages;
                }
            }
            else
            {
                if (page < SoA.MaxNecronomiconPages)
                {
                    page++;
                }
                else
                {
                    page = 0;
                }
            }
            SoA.LogInfo(player.Slayer().slayerMode, "Slayer mode enabled: ");
            SoA.LogInfo(player.Slayer().slayerSet, "Entropy set bonus active: ");
            SoA.LogInfo(player.Slayer().soulCrystalNames, "Soul crystals: ");
            SoA.LogInfo(player.Slayer().soulTeleports, "Soul crystal teleports: ");
            SoA.LogInfo(player.Slayer().lunaticCircleFragments, "Cultist circle fragments: ");
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            frame = new(0, 0, 68, 64);
            Main.instance.LoadItem(ModContent.ItemType<Necronomicon>());
            var book = TextureAssets.Item[ModContent.ItemType<Necronomicon>()].Value;
            if (page > 0)
            {
                frame.Y += 64;
            }

            spriteBatch.Draw(book, position, frame, drawColor, 0f, frame.Size() * 0.5f, scale * 2f, SpriteEffects.None, 0f);
            return false;
        }

        public override bool? UseItem(Player player)
        {
            if (page == 1)
            {
                if (!player.Slayer().slayerMode)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Slayer mode enabled for " + player.name), Color.White);
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                    player.Slayer().slayerMode = true;
                }
                else
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Slayer mode disabled for " + player.name), Color.White);
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                    player.Slayer().slayerMode = false;
                }
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.Slayer();

            if (page == 0)
            {
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "TableOfContents", "Table of Contents:"));
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "PageList", "General Info"));
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "PageList", "Soul Crystal Info"));

                // Absorbed List
                for (int i = 0; i < entries.Count; i++)
                {
                    PageEntry entry = entries[i];
                    if (slayer.HasSoulCrystal(entry.crystalItem) || SoA.ClientConfig.entryView)
                    {
                        tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "PageList", $"{entry.entryName} ({entry.mod})")
                        {
                            OverrideColor = entry.entryColor
                        });
                    }
                }
            }
            if (page == 1)
            {
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Page", Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.GenericInfo")));
                if (Main.LocalPlayer.Slayer().slayerMode)
                {
                    tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "SlayerMode", Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.Active"))
                    {
                        OverrideColor = Color.Red
                    });
                }
            }
            if (page == 2)
            {
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Page", Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.SoulCrystalInfo")));
            }

            // Soul Crystal effects
            if (page >= 3)
            {
                PageEntry entry = entries[page - 3];
                if (slayer.HasSoulCrystal(entry.crystalItem) || SoA.ClientConfig.entryView)
                {
                    tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Page", $"{entry.EntryText()}")
                    {
                        OverrideColor = entry.entryColor
                    });
                }
            }

            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "TurnPage", "----------\n" +
                "Right click to turn the page"));
            if (page > 0)
            {
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "TurnPageBack", "Hold Left Shift and Right Click to turn the page backward"));
            }
            if (page > 0)
            {
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "CloseBook", "Hold Left Alt and Right Click to return to Table of Contents"));
            }
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateInventory(Player player)
        {
            SlayerPlayer slayer = player.Slayer();
            if (SoA.ClientConfig.entryView)
            {
                return;
            }
            else if (page >= 3)
            {
                if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.HasSoulCrystal(entry.crystalItem))
                    {
                        page--;
                    }
                }
                else
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.HasSoulCrystal(entry.crystalItem))
                    {
                        page++;
                    }
                    if (page > SoA.MaxNecronomiconPages)
                    {
                        page = 0;
                    }
                }
            }
        }
    }
}