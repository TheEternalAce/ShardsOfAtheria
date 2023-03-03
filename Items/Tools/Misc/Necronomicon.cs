using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static ShardsOfAtheria.Utilities.Entry;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class Necronomicon : ModItem
    {
        public int page;
        public static Asset<Texture2D> book;

        public override void SetStaticDefaults()
        {
            book = ModContent.Request<Texture2D>(Texture + "_Open");

            SacrificeTotal = 1;
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
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (page > 0)
            {
                spriteBatch.Draw(book.Value, position - book.Size() * 0.5f + TextureAssets.Item[Item.type].Size() * 0.5f, null, drawColor * 0.95f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                return false;
            }

            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
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
                    if (slayer.soulCrystals.Contains(entry.crystalItem) || ModContent.GetInstance<ShardsClientConfig>().entryView)
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
            if (page > ShardsOfAtheriaMod.MaxNecronomiconPages)
            {
                page = 0;
            }

            if (page >= 3)
            {
                PageEntry entry = entries[page - 3];
                if (slayer.soulCrystals.Contains(entry.crystalItem) || ModContent.GetInstance<ShardsClientConfig>().entryView)
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
            if (page > ShardsOfAtheriaMod.MaxNecronomiconPages)
            {
                page = ShardsOfAtheriaMod.MaxNecronomiconPages;
            }
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (ModContent.GetInstance<ShardsClientConfig>().entryView)
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
                    if (page > ShardsOfAtheriaMod.MaxNecronomiconPages)
                    {
                        page = ShardsOfAtheriaMod.MaxNecronomiconPages;
                    }
                }
                else
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.soulCrystals.Contains(entry.crystalItem))
                    {
                        page++;
                    }
                    if (page > ShardsOfAtheriaMod.MaxNecronomiconPages)
                    {
                        page = 0;
                    }
                }
            }
        }
    }
}