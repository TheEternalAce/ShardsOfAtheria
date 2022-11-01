using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SoulCrystals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
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
            NewEntry("Terraria", "King Slime", KingSoulCrystal.tip, Color.Blue, ModContent.ItemType<KingSoulCrystal>());
            NewEntry("Terraria", "Eye of Cthulhu", EyeSoulCrystal.tip, Color.Red, ModContent.ItemType<EyeSoulCrystal>());
            NewEntry("Terraria", "Brain of Cthulhu", BrainSoulCrystal.tip, Color.LightPink, ModContent.ItemType<BrainSoulCrystal>());
            NewEntry("Terraria", "Eater of Worlds", EaterSoulCrystal.tip, Color.Purple, ModContent.ItemType<EaterSoulCrystal>());
            NewEntry("Terraria", "Queen Bee", BeeSoulCrystal.tip, Color.Yellow, ModContent.ItemType<BeeSoulCrystal>());
            NewEntry("Terraria", "Skeletron", SkullSoulCrystal.tip, new Color(130, 130, 90), ModContent.ItemType<SkullSoulCrystal>());
            NewEntry("Shards of Atheria", "Lightning Valkyrie, Nova Stellar", ValkyrieSoulCrystal.tip, Color.DeepSkyBlue, ModContent.ItemType<ValkyrieSoulCrystal>());
            NewEntry("Terraria", "Deerclops", DeerclopsSoulCrystal.tip, Color.MediumPurple, ModContent.ItemType<DeerclopsSoulCrystal>());
            NewEntry("Terraria", "Wall of Flesh", WallSoulCrystal.tip, Color.MediumPurple, ModContent.ItemType<WallSoulCrystal>());
            NewEntry("Terraria", "Queen Slime", QueenSoulCrystal.tip, Color.Pink, ModContent.ItemType<QueenSoulCrystal>());
            NewEntry("Terraria", "Destroyer", DestroyerSoulCrystal.tip, Color.Gray, ModContent.ItemType<DestroyerSoulCrystal>());
            NewEntry("Terraria", "Skeletron Prime", PrimeSoulCrystal.tip, Color.Gray, ModContent.ItemType<PrimeSoulCrystal>());
            NewEntry("Terraria", "The Twins", TwinsSoulCrystal.tip, Color.Gray, ModContent.ItemType<TwinsSoulCrystal>());
            NewEntry("Terraria", "Plantera", PlantSoulCrystal.tip, Color.Pink, ModContent.ItemType<PlantSoulCrystal>());
            NewEntry("Terraria", "Golem", GolemSoulCrystal.tip, Color.DarkOrange, ModContent.ItemType<GolemSoulCrystal>());
            NewEntry("Terraria", "Duke Fishron", DukeSoulCrystal.tip, Color.SeaGreen, ModContent.ItemType<DukeSoulCrystal>());
            NewEntry("Terraria", "Empress of Light", EmpressSoulCrystal.tip, Main.DiscoColor, ModContent.ItemType<EmpressSoulCrystal>());
            NewEntry("Terraria", "Lunatic Cultist", LunaticSoulCrystal.tip, Color.Blue, ModContent.ItemType<LunaticSoulCrystal>());
            NewEntry("Terraria", "Moon Lord", LordSoulCrystal.tip, Color.LightCyan, ModContent.ItemType<LordSoulCrystal>());
            NewEntry("Shards of Atheria", "Senterra, Atherial Land", WipEntry(), Color.Green, ItemID.None);
            NewEntry("Shards of Atheria", "Genesis, Atherial Time", WipEntry(), Color.BlueViolet, ItemID.None);
            NewEntry("Shards of Atheria", "Elizabeth Norman, Death", WipEntry(), Color.DarkGray, ItemID.None);
        }

        public static string WipEntry()
        {
            return "This Soul Crystal needs further research. Please wait for this boss to be introduced into the mod.";
        }
    }
}
