using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.ShardsUI.EntropicSelection;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static ShardsOfAtheria.Systems.SlayerSystem;

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
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt) && page > 0)
                page = 0;
            else if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                if (page > 0) page--;
                else page = SlayerSystem.MaxNecronomiconPages;
            }
            else
            {
                if (page < SlayerSystem.MaxNecronomiconPages) page++;
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
                TooltipLine line1 = new(Mod, "TableOfContents", this.GetLocalizedValue("TableOfContents"));
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line1);

                // Absorbed List
                for (int i = 0; i < entries.Count; i++)
                {
                    PageEntry entry = entries[i];
                    if (slayer.HasSoulCrystal(entry.crystalItem) || SoA.ClientConfig.entryView)
                    {
                        TooltipLine line2 = new(Mod, "SoulCrystalList", $"{entry.entryName} ({entry.mod})")
                        {
                            OverrideColor = entry.entryColor
                        };
                        tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line2);
                    }
                }
            }
            if (page == 1)
            {
                TooltipLine line = new(Mod, "General", this.GetLocalizedValue("GenericInfo"));
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
            if (page == 2)
            {
                TooltipLine line = new(Mod, "SoulCrystals", this.GetLocalizedValue("SoulCrystalInfo"));
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }

            // Soul Crystal effects
            if (page >= 3)
            {
                PageEntry entry = entries[page - 3];
                if (slayer.HasSoulCrystal(entry.crystalItem) || SoA.ClientConfig.entryView)
                {
                    tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "SoulCrystalEntry", entry.EntryText)
                    {
                        OverrideColor = entry.entryColor
                    });
                }
            }

            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "BreakLine", "----------"));
            if (page > 0)
            {
                TooltipLine line = new(Mod, "TurnPageBack", this.GetLocalizedValue("PagesGuide"));
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
            else
            {
                TooltipLine line = new(Mod, "TurnPageBack", this.GetLocalizedValue("BookGuide"));
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
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
                    if (!slayer.HasSoulCrystal(entry.crystalItem)) page--;
                }
                else
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.HasSoulCrystal(entry.crystalItem)) page++;
                    if (page > SlayerSystem.MaxNecronomiconPages) page = 0;
                }
            }
        }

        int transformTimer;
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (SlayerBookSelectionUI.Instance.UIActive) return;
            if (page > 0)
            {
                bool entropyFragment = false;
                bool amethysts = false;
                foreach (Item item in Main.item)
                {
                    if (item.active && item.whoAmI != Item.whoAmI)
                    {
                        if (item.Distance(Item.Center) < 100)
                        {
                            if (item.type == ModContent.ItemType<FragmentEntropy>() && item.stack >= 10)
                                entropyFragment = true;
                            if (item.type == ItemID.Amethyst && item.stack >= 5) amethysts = true;
                        }
                    }
                }
                if (entropyFragment && amethysts)
                {
                    if (++transformTimer > 120)
                    {
                        foreach (Item item in Main.item)
                        {
                            if (item.active && item.whoAmI != Item.whoAmI && item.Hitbox.Intersects(Item.Hitbox))
                            {
                                if (item.type == ModContent.ItemType<FragmentEntropy>() && item.stack >= 10)
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
                        SlayerBookSelectionUI.Instance.Enable();
                        ShardsHelpers.DustRing(Item.Center, 18f, DustID.ShadowbeamStaff);
                    }
                }
            }
        }
    }
}