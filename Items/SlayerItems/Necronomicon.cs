using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using ShardsOfAtheria.Players;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static ShardsOfAtheria.Items.SlayerItems.Entry;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class Entry
    {
        public static List<PageEntry> entries = new();

        public static void NewEntry(string mod, string name, string tooltip, int crystalItem)
        {
            entries.Add(new PageEntry(mod, name, tooltip, Color.White, crystalItem));
            ShardsOfAtheria.MaxNecronomiconPages++;
        }

        public static void NewEntry(string mod, string name, string tooltip, Color pageColor, int crystalItem)
        {
            entries.Add(new PageEntry(mod, name, tooltip, pageColor, crystalItem));
            ShardsOfAtheria.MaxNecronomiconPages++;
        }

        public struct PageEntry
        {
            public string mod = "";
            public string soulCrystalTooltip = "";
            public string entryName = "";
            public Color entryColor = Color.White;
            public int crystalItem = 0;

            public PageEntry(string mod, string entryName, string soulCrystalTooltip, Color entryColor, int crystalItem)
            {
                this.mod = mod;
                this.entryName = entryName;
                this.soulCrystalTooltip = soulCrystalTooltip;
                this.entryColor = entryColor;
                this.crystalItem = crystalItem;

            }

            public string EntryText()
            {
                return $"{entryName} ({mod})\n" +
                    $"{soulCrystalTooltip}";
            }
        }

        public static void IncludedEntries()
        {
            NewEntry(nameof(Terraria), "King Slime", KingSoulCrystal.tip, Color.Blue, ModContent.ItemType<KingSoulCrystal>());
            NewEntry(nameof(Terraria), "Eye of Cthulhu", EyeSoulCrystal.tip, Color.Red, ModContent.ItemType<EyeSoulCrystal>());
            NewEntry(nameof(Terraria), "Brain of Cthulhu", BrainSoulCrystal.tip, Color.LightPink, ModContent.ItemType<BrainSoulCrystal>());
            NewEntry(nameof(Terraria), "Eater of Worlds", EaterSoulCrystal.tip, Color.Purple, ModContent.ItemType<EaterSoulCrystal>());
            NewEntry(nameof(Terraria), "Queen Bee", BeeSoulCrystal.tip, Color.Yellow, ModContent.ItemType<BeeSoulCrystal>());
            NewEntry(nameof(Terraria), "Skeletron", SkullSoulCrystal.tip, new Color(130, 130, 90), ModContent.ItemType<SkullSoulCrystal>());
            NewEntry(nameof(ShardsOfAtheria), "Lightning Valkyrie, Nova Stellar", ValkyrieSoulCrystal.tip, Color.DeepSkyBlue, ModContent.ItemType<ValkyrieSoulCrystal>());
            NewEntry(nameof(Terraria), "Deerclops", DeerclopsSoulCrystal.tip, Color.MediumPurple, ModContent.ItemType<DeerclopsSoulCrystal>());
            NewEntry(nameof(Terraria), "Wall of Flesh", WallSoulCrystal.tip, Color.MediumPurple, ModContent.ItemType<WallSoulCrystal>());
            NewEntry(nameof(Terraria), "Queen Slime", QueenSoulCrystal.tip, Color.Pink, ModContent.ItemType<QueenSoulCrystal>());
            NewEntry(nameof(Terraria), "Destroyer", DestroyerSoulCrystal.tip, Color.Gray, ModContent.ItemType<DestroyerSoulCrystal>());
            NewEntry(nameof(Terraria), "Skeletron Prime", PrimeSoulCrystal.tip, Color.Gray, ModContent.ItemType<PrimeSoulCrystal>());
            NewEntry(nameof(Terraria), "The Twins", TwinsSoulCrystal.tip, Color.Gray, ModContent.ItemType<TwinsSoulCrystal>());
            NewEntry(nameof(Terraria), "Plantera", PlantSoulCrystal.tip, Color.Pink, ModContent.ItemType<PlantSoulCrystal>());
            NewEntry(nameof(Terraria), "Golem", GolemSoulCrystal.tip, Color.DarkOrange, ModContent.ItemType<GolemSoulCrystal>());
            NewEntry(nameof(Terraria), "Duke Fishron", DukeSoulCrystal.tip, Color.SeaGreen, ModContent.ItemType<DukeSoulCrystal>());
            NewEntry(nameof(Terraria), "Empress of Light", EmpressSoulCrystal.tip, Main.DiscoColor, ModContent.ItemType<EmpressSoulCrystal>());
            NewEntry(nameof(Terraria), "Lunatic Cultist", LunaticSoulCrystal.tip, Color.Blue, ModContent.ItemType<LunaticSoulCrystal>());
            NewEntry(nameof(Terraria), "Moon Lord", LordSoulCrystal.tip, Color.LightCyan, ModContent.ItemType<LordSoulCrystal>());
            NewEntry(nameof(ShardsOfAtheria), "Senterra, Atherial Land", WipEntry(), Color.Green, ItemID.None);
            NewEntry(nameof(ShardsOfAtheria), "Genesis, Atherial Time", WipEntry(), Color.BlueViolet, ItemID.None);
            NewEntry(nameof(ShardsOfAtheria), "Elizabeth Norman, Death", WipEntry(), Color.DarkGray, ItemID.None);
            Console.WriteLine(entries[0].EntryText());
        }

        public static string WipEntry()
        {
            return "This Soul Crystal needs further research. Please wait for this boss to be introduced into the mod.";
        }
    }

    public class Necronomicon : ModItem
    {
        public int page;
        public static Asset<Texture2D> book;

        public override void SetStaticDefaults()
        {
            book = ModContent.Request<Texture2D>(Texture + "_Open");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SoAGlobalItem.SlayerItem.Add(Type);
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
                    if (slayer.soulCrystals.Contains(entry.crystalItem))
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
                tooltips.Add(new TooltipLine(Mod, "Page", "General Info:\n" +
                    "Use on this page to toggle Slayer mode. Cannot be used while a boss is alive.\n" +
                    "Bosses will drop every item in its loot table (in a stack of 1000 if the item is stackable), its slayer mode exclusive item and its Soul Crystal.\n" +
                    "This boss is considered slain and cannot be fought again.\n" +
                    "While Slayer mode is active, taking damage will reduce your defense. Defense will regenerate after a period of time."));
                if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
                {
                    tooltips.Add(new TooltipLine(Mod, "SlayerMode", "Slayer Mode Active")
                    {
                        OverrideColor = Color.Red
                    });
                }
            }
            if (page == 2)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Soul Crystal Info:\n" +
                    "Absorbing a boss' Soul Crystal gives you it's powers. Be warned: absorbing too many Soul Crystals may descend you into madness.\n" +
                    "Soul Crystals are removable by making a Soul Extracting Dagger.\n" +
                    "Certain combinations of Soul Crystals and the correct Soul Bond will cause a synergy between the Soul Crystals and new special effects will occur."));
            }

            // Soul Crystal effects
            if (page > ShardsOfAtheria.MaxNecronomiconPages)
            {
                page = 0;
            }

            if (page >= 3)
            {
                PageEntry entry = entries[page - 3];
                if (slayer.soulCrystals.Contains(entry.crystalItem))
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
            if (page > ShardsOfAtheria.MaxNecronomiconPages)
            {
                page = ShardsOfAtheria.MaxNecronomiconPages;
            }
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (ModContent.GetInstance<ShardsConfigClientSide>().entryView)
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
                    if (page > ShardsOfAtheria.MaxNecronomiconPages)
                    {
                        page = 23;
                    }
                }
                else
                {
                    PageEntry entry = entries[page - 3];
                    if (!slayer.soulCrystals.Contains(entry.crystalItem))
                    {
                        page++;
                    }
                    if (page > ShardsOfAtheria.MaxNecronomiconPages)
                    {
                        page = 0;
                    }
                }
            }
        }
    }
}