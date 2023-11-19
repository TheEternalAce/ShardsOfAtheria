using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace ShardsOfAtheria.Utilities
{
    public class Entry
    {
        public static bool entriesLoaded = false;
        public static List<PageEntry> entries = new();

        public static void NewEntry(string mod, string name, string tooltip, string crystalItem)
        {
            NewEntry(mod, name, tooltip, Color.White, crystalItem);
        }
        public static void NewEntry(string mod, string name, string tooltip, Color pageColor, string crystalItem)
        {
            entries.Add(new PageEntry(mod, name, tooltip, pageColor, crystalItem));
            SoA.MaxNecronomiconPages++;
        }
        public static void NewEntryWithKeys(string mod, string nameKey, string tooltipKey, Color pageColor, string crystalItem)
        {
            entries.Add(new PageEntry(mod, Language.GetTextValue(nameKey), Language.GetTextValue(tooltipKey), pageColor, crystalItem));
            SoA.MaxNecronomiconPages++;
        }

        public struct PageEntry
        {
            public string mod = "";
            public string soulCrystalTooltip = "";
            public string entryName = "";
            public Color entryColor = Color.White;
            public string crystalItem = "";

            public PageEntry(string mod, string entryName, string soulCrystalTooltip, Color entryColor, string crystalItem)
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
            string KeyBase = "Mods.ShardsOfAtheria.Items.";
            NewEntry("Terraria", "King Slime", Language.GetTextValue(KeyBase + "KingSoulCrystal.Tooltip"), Color.Blue, "KingSoulCrystal");
            NewEntry("Terraria", "Eye of Cthulhu", Language.GetTextValue(KeyBase + "EyeSoulCrystal.Tooltip"), Color.Red, "EyeSoulCrystal");
            NewEntry("Terraria", "Brain of Cthulhu", Language.GetTextValue(KeyBase + "BrainSoulCrystal.Tooltip"), Color.LightPink, "BrainSoulCrystal");
            NewEntry("Terraria", "Eater of Worlds", Language.GetTextValue(KeyBase + "EaterSoulCrystal.Tooltip"), Color.Purple, "EaterSoulCrystal");
            NewEntry("Terraria", "Queen Bee", Language.GetTextValue(KeyBase + "BeeSoulCrystal.Tooltip"), Color.Yellow, "BeeSoulCrystal");
            NewEntry("Terraria", "Skeletron", Language.GetTextValue("Mods.ShardsOfAtheria.Item.SkullSoulCrystal.Tooltip"), new Color(130, 130, 90), "SkullSoulCrystal");
            NewEntry("Shards of Atheria", "Lightning Valkyrie, Nova Stellar", Language.GetTextValue(KeyBase + "ValkyrieSoulCrystal.Tooltip"), Color.DeepSkyBlue, "ValkyrieSoulCrystal");
            NewEntry("Terraria", "Deerclops", Language.GetTextValue(KeyBase + "DeerclopsSoulCrystal.Tooltip"), Color.MediumPurple, "DeerclopsSoulCrystal");
            NewEntry("Terraria", "Wall of Flesh", Language.GetTextValue(KeyBase + "WallSoulCrystal.Tooltip"), Color.MediumPurple, "WallSoulCrystal");
            NewEntry("Terraria", "Queen Slime", Language.GetTextValue(KeyBase + "QueenSoulCrystal.Tooltip"), Color.Pink, "QueenSoulCrystal");
            NewEntry("Terraria", "Destroyer", Language.GetTextValue(KeyBase + "DestroyerSoulCrystal.Tooltip"), Color.Gray, "DestroyerSoulCrystal");
            NewEntry("Terraria", "Skeletron Prime", Language.GetTextValue(KeyBase + "PrimeSoulCrystal.Tooltip"), Color.Gray, "PrimeSoulCrystal");
            NewEntry("Terraria", "The Twins", Language.GetTextValue(KeyBase + "TwinsSoulCrystal.Tooltip"), Color.Gray, "TwinsSoulCrystal");
            NewEntry("Terraria", "Plantera", Language.GetTextValue(KeyBase + "PlantSoulCrystal.Tooltip"), Color.Pink, "PlantSoulCrystal");
            NewEntry("Terraria", "Golem", Language.GetTextValue(KeyBase + "GolemSoulCrystal.Tooltip"), Color.DarkOrange, "GolemSoulCrystal");
            NewEntry("Terraria", "Duke Fishron", Language.GetTextValue(KeyBase + "DukeSoulCrystal.Tooltip"), Color.SeaGreen, "DukeSoulCrystal");
            NewEntry("Terraria", "Empress of Light", Language.GetTextValue(KeyBase + "EmpressSoulCrystal.Tooltip"), Main.DiscoColor, "EmpressSoulCrystal");
            NewEntry("Terraria", "Lunatic Cultist", Language.GetTextValue(KeyBase + "LunaticSoulCrystal.Tooltip"), Color.Blue, "LunaticSoulCrystal");
            NewEntry("Terraria", "Moon Lord", Language.GetTextValue(KeyBase + "LordSoulCrystal.Tooltip"), Color.LightCyan, "LordSoulCrystal");
            NewEntry("Shards of Atheria", "Senterra, Atherial Land", WipEntry(), Color.Green, "LandSoulCrystal");
            NewEntry("Shards of Atheria", "Genesis, Atherial Time", WipEntry(), Color.BlueViolet, "TimeSoulCrystal");
            NewEntry("Shards of Atheria", "Elizabeth Norman, Death", WipEntry(), Color.DarkGray, "DeathSoulCrystal");
        }

        public static string WipEntry()
        {
            return Language.GetTextValue("Mods.ShardsOfAtheria.Necronomicon.WipSoulEntry");
        }
    }
}
