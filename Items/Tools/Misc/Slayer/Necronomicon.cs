using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Config;
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
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
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
                page = 0;
            else if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) && page > 0)
                page--;
            else page++;

            SoA.Log("Slayer mode? ", player.Slayer().slayerMode);
            SoA.Log("Entropy set bonus? ", player.Slayer().slayerSet);
            SoA.Log("Soul crystals: ", player.Slayer().soulCrystals);
            SoA.Log("Soul crystal teleports: ", player.Slayer().soulTeleports);
            SoA.Log("Cultist circle fragments: ", player.Slayer().lunaticCircleFragments);
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
                if (!player.GetModPlayer<SlayerPlayer>().slayerMode)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Slayer mode enabled for " + player.name), Color.White);
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                    player.GetModPlayer<SlayerPlayer>().slayerMode = true;
                }
                else
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Slayer mode disabled for " + player.name), Color.White);
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                    player.GetModPlayer<SlayerPlayer>().slayerMode = false;
                }
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();

            if (page == 0)
            {
                tooltips.Add(new TooltipLine(Mod, "TableOfContents", "Table of Contents:"));
                tooltips.Add(new TooltipLine(Mod, "PageList", "General Info"));
                tooltips.Add(new TooltipLine(Mod, "PageList", "Soul Crystal Info"));

                // Absorbed PageList
                for (int i = 0; i < entries.Count; i++)
                {
                    PageEntry entry = entries[i];
                    if (slayer.soulCrystals.Contains(entry.crystalItem) || SoA.ClientConfig.entryView)
                    {
                        tooltips.Add(new TooltipLine(Mod, "PageList", $"{entry.entryName} ({entry.mod})")
                        {
                            OverrideColor = entry.entryColor
                        });
                    }
                }
            }
            if (page == 1)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.GenericInfo")));
                if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
                {
                    tooltips.Add(new TooltipLine(Mod, "SlayerMode", Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.Active"))
                    {
                        OverrideColor = Color.Red
                    });
                }
            }
            if (page == 2)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.SoulCrystalInfo")));
            }

            // Soul Crystal effects
            if (page > SoA.MaxNecronomiconPages)
            {
                page = 0;
            }

            if (page >= 3)
            {
                PageEntry entry = entries[page - 3];
                if (slayer.soulCrystals.Contains(entry.crystalItem) || SoA.ClientConfig.entryView)
                {
                    tooltips.Add(new TooltipLine(Mod, "Page", $"{entry.EntryText()}")
                    {
                        OverrideColor = entry.entryColor
                    });
                }
            }

            tooltips.Add(new TooltipLine(Mod, "TurnPage", "----------\n" +
                "Right click to turn the page"));
            if (page > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "TurnPageBack", "Hold Left Shift and Right Click to turn the page backward"));
            }
            if (page > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "CloseBook", "Hold Left Alt and Right Click to return to Table of Contents"));
            }
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateInventory(Player player)
        {
            if (page > SoA.MaxNecronomiconPages)
            {
                page = SoA.MaxNecronomiconPages;
            }
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (SoA.ClientConfig.entryView)
            {
                return;
            }
            else if (page >= 3)
            {
                if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.soulCrystals.Contains(entry.crystalItem))
                    {
                        page--;
                    }
                    if (page > SoA.MaxNecronomiconPages)
                    {
                        page = SoA.MaxNecronomiconPages;
                    }
                }
                else
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.soulCrystals.Contains(entry.crystalItem))
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