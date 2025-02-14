using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.ShardsUI.EntropicSelection;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
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

            Item.rare = ItemDefaults.RaritySlayer;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddRecipeGroup(ShardsRecipes.EvilBar, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return false;
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
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt) && page > 0) page = 0;
            else if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                if (page > 0) page--;
                else page = SoA.MaxNecronomiconPages;
            }
            else
            {
                if (page < SoA.MaxNecronomiconPages) page++;
                else page = 0;
            }
            SoA.LogInfo(player.Slayer().slayerMode, "Slayer mode enabled: ");
            SoA.LogInfo(player.Slayer().slayerSet, "Entropy set bonus active: ");
            SoA.LogInfo(player.Slayer().soulCrystalNames, "Soul crystals: ");
            SoA.LogInfo(player.Slayer().soulTeleports, "Soul crystal teleports: ");
            SoA.LogInfo(player.Slayer().lunaticCircleFragments, "Cultist circle fragments: ");
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Rectangle frame = new(0, 0, 68, 64);
            Main.instance.LoadItem(ModContent.ItemType<Necronomicon>());
            var book = TextureAssets.Item[ModContent.ItemType<Necronomicon>()].Value;
            if (page > 0) frame.Y += 64;

            spriteBatch.Draw(book, Item.position - Main.screenPosition, frame, lightColor, 0f, new Vector2(13, 4), scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            frame = new(0, 0, 68, 64);
            Main.instance.LoadItem(ModContent.ItemType<Necronomicon>());
            var book = TextureAssets.Item[ModContent.ItemType<Necronomicon>()].Value;
            if (page > 0) frame.Y += 64;

            spriteBatch.Draw(book, position, frame, drawColor, 0f, frame.Size() * 0.5f, scale * 2f, SpriteEffects.None, 0f);
            return false;
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
            if (page == 1) tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Page", ShardsHelpers.LocalizeNecronomicon("GenericInfo")));
            if (page == 2) tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Page", ShardsHelpers.LocalizeNecronomicon("SoulCrystalInfo")));

            // Soul Crystal effects
            if (page >= 3)
            {
                PageEntry entry = entries[page - 3];
                if (slayer.HasSoulCrystal(entry.crystalItem) || SoA.ClientConfig.entryView)
                {
                    tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Page", entry.EntryText)
                    {
                        OverrideColor = entry.entryColor
                    });
                }
            }

            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "TurnPage", "----------\n" + ShardsHelpers.LocalizeNecronomicon("TurnPage")));
            if (page > 0) tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "TurnPageBack", ShardsHelpers.LocalizeNecronomicon("TurnPageBack")));
            if (page > 0) tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "CloseBook", ShardsHelpers.LocalizeNecronomicon("TurnToC")));
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
                    if (!slayer.HasSoulCrystal(entry.crystalItem)) page--;
                }
                else
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.HasSoulCrystal(entry.crystalItem)) page++;
                    if (page > SoA.MaxNecronomiconPages) page = 0;
                }
            }
        }

        int transformTimer;
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (SlayerBookSelectionUI.Instance.UIActive) return;
            if (page > 0)
            {
                bool evilBars = false;
                bool amethysts = false;
                foreach (Item item in Main.item)
                {
                    if (item.active && item.whoAmI != Item.whoAmI && item.Hitbox.Intersects(Item.Hitbox))
                    {
                        if ((item.type == ItemID.DemoniteBar || item.type == ItemID.CrimtaneBar) && item.stack >= 10) evilBars = true;
                        if (item.type == ItemID.Amethyst && item.stack >= 5) amethysts = true;
                    }
                }
                if (evilBars && amethysts)
                {
                    if (++transformTimer > 120)
                    {
                        foreach (Item item in Main.item)
                        {
                            if (item.active && item.whoAmI != Item.whoAmI && item.Hitbox.Intersects(Item.Hitbox))
                            {
                                if ((item.type == ItemID.DemoniteBar || item.type == ItemID.CrimtaneBar) && item.stack >= 10)
                                {
                                    item.stack -= 10;
                                    if (item.stack <= 0) item.TurnToAir();
                                }
                                if ((item.type == ItemID.Amethyst) && item.stack >= 5)
                                {
                                    item.stack -= 5;
                                    if (item.stack <= 0) item.TurnToAir();
                                }
                            }
                        }
                        transformTimer = 0;
                        //item.stack -= 10;
                        //if (item.stack <= 0) item.TurnToAir();
                        SlayerBookSelectionUI.Instance.Enable();
                        ShardsHelpers.DustRing(Item.Center, 18f, DustID.ShadowbeamStaff);
                    }
                }
            }
        }
    }
}