using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SoulCrystals;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public class Entry
    {
        public static bool entriesLoaded = false;
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
            NewEntry("Terraria", "King Slime", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.KingSoulCrystal"), Color.Blue, ModContent.ItemType<KingSoulCrystal>());
            NewEntry("Terraria", "Eye of Cthulhu", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.EyeSoulCrystal"), Color.Red, ModContent.ItemType<EyeSoulCrystal>());
            NewEntry("Terraria", "Brain of Cthulhu", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.BrainSoulCrystal"), Color.LightPink, ModContent.ItemType<BrainSoulCrystal>());
            NewEntry("Terraria", "Eater of Worlds", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.EaterSoulCrystal"), Color.Purple, ModContent.ItemType<EaterSoulCrystal>());
            NewEntry("Terraria", "Queen Bee", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.BeeSoulCrystal"), Color.Yellow, ModContent.ItemType<BeeSoulCrystal>());
            NewEntry("Terraria", "Skeletron", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.SkullSoulCrystal"), new Color(130, 130, 90), ModContent.ItemType<SkullSoulCrystal>());
            NewEntry("Shards of Atheria", "Lightning Valkyrie, Nova Stellar", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.ValkyrieSoulCrystal"),
                Color.DeepSkyBlue, ModContent.ItemType<ValkyrieSoulCrystal>());
            NewEntry("Terraria", "Deerclops", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.DeerclopsSoulCrystal"), Color.MediumPurple, ModContent.ItemType<DeerclopsSoulCrystal>());
            NewEntry("Terraria", "Wall of Flesh", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.WallSoulCrystal"), Color.MediumPurple, ModContent.ItemType<WallSoulCrystal>());
            NewEntry("Terraria", "Queen Slime", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.QueenSoulCrystal"), Color.Pink, ModContent.ItemType<QueenSoulCrystal>());
            NewEntry("Terraria", "Destroyer", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.DestroyerSoulCrystal"), Color.Gray, ModContent.ItemType<DestroyerSoulCrystal>());
            NewEntry("Terraria", "Skeletron Prime", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.PrimeSoulCrystal"), Color.Gray, ModContent.ItemType<PrimeSoulCrystal>());
            NewEntry("Terraria", "The Twins", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.TwinsSoulCrystal"), Color.Gray, ModContent.ItemType<TwinsSoulCrystal>());
            NewEntry("Terraria", "Plantera", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.PlantSoulCrystal"), Color.Pink, ModContent.ItemType<PlantSoulCrystal>());
            NewEntry("Terraria", "Golem", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GolemSoulCrystal"), Color.DarkOrange, ModContent.ItemType<GolemSoulCrystal>());
            NewEntry("Terraria", "Duke Fishron", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.DukeSoulCrystal"), Color.SeaGreen, ModContent.ItemType<DukeSoulCrystal>());
            NewEntry("Terraria", "Empress of Light", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.EmpressSoulCrystal"), Main.DiscoColor, ModContent.ItemType<EmpressSoulCrystal>());
            NewEntry("Terraria", "Lunatic Cultist", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.LunaticSoulCrystal"), Color.Blue, ModContent.ItemType<LunaticSoulCrystal>());
            NewEntry("Terraria", "Moon Lord", Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.LordSoulCrystal"), Color.LightCyan, ModContent.ItemType<LordSoulCrystal>());
            NewEntry("Shards of Atheria", "Senterra, Atherial Land", WipEntry(), Color.Green, ItemID.None);
            NewEntry("Shards of Atheria", "Genesis, Atherial Time", WipEntry(), Color.BlueViolet, ItemID.None);
            NewEntry("Shards of Atheria", "Elizabeth Norman, Death", WipEntry(), Color.DarkGray, ItemID.None);
        }

        public static string WipEntry()
        {
            return Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.WipSoulEntry");
        }
    }
}
