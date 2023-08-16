using BattleNetworkElements;
using BattleNetworkElements.Utilities;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.NPCs.Misc;
using ShardsOfAtheria.NPCs.Town.TheArchivist;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
using ShardsOfAtheria.NPCs.Variant.Harpy;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ShardsOfAtheria.CrossMod.ElementsMod
{
    [JITWhenModsEnabled("BattleNetworkElements")]
    internal class SetNPCElements : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.ElementModEnabled;
        }

        static readonly List<int> Fire = new()
        {
            NPCType<CorruptHarpy>(),
            NPCType<VoidHarpy>(),
            NPCType<AreusMortar>(),
        };
        static readonly List<int> Aqua = new()
        {
            NPCType<Death>(),
            NPCType<Creeper>(),
            NPCType<CrimsonHarpy>(),
            NPCType<OceanHarpy>(),
            NPCType<SnowHarpy>(),
        };
        static readonly List<int> Elec = new()
        {
            NPCType<NovaStellar>(),
            NPCType<Atherian>(),
            NPCType<DesertHarpy>(),
            NPCType<AreusMortar>(),
        };
        static readonly List<int> Wood = new()
        {
            NPCType<Death>(),
            NPCType<Creeper>(),
            NPCType<Archivist>(),
            NPCType<CorruptHarpy>(),
            NPCType<CrimsonHarpy>(),
            NPCType<ForestHarpy>(),
        };

        [JITWhenModsEnabled("BattleNetworkElements")]
        public override void SetStaticDefaults()
        {
            foreach (int i in Fire)
            {
                i.AddFireNPC();
            }
            foreach (int i in Aqua)
            {
                i.AddAquaNPC();
            }
            foreach (int i in Elec)
            {
                i.AddElecNPC();
            }
            foreach (int i in Wood)
            {
                i.AddWoodNPC();
            }
        }

        [JITWhenModsEnabled("BattleNetworkElements")]
        public override void SetDefaults(NPC npc)
        {
            if (SoA.ElementModEnabled)
            {
                int type = npc.type;
                if (type == NPCType<CorruptHarpy>() ||
                    type == NPCType<VoidHarpy>())
                {
                    npc.ElementMultipliers() = Element.FireMultipliers;
                }
                if (type == NPCType<Creeper>() ||
                    type == NPCType<CrimsonHarpy>() ||
                    type == NPCType<OceanHarpy>())
                {
                    npc.ElementMultipliers() = Element.AquaMultipliers;
                }
                if (type == NPCType<DesertHarpy>())
                {
                    npc.ElementMultipliers() = Element.ElecMultipliers;
                }
                if (type == NPCType<Archivist>() ||
                    type == NPCType<ForestHarpy>())
                {
                    npc.ElementMultipliers() = Element.WoodMultipliers;
                }
                // Custom multipliers
                if (type == NPCType<Death>())
                {
                    npc.ElementMultipliers() = new[] { 1.0f, 0.8f, 1.5f, 0.8f };
                }
                if (type == NPCType<NovaStellar>())
                {
                    npc.ElementMultipliers() = new[] { 2.0f, 0.8f, 0.8f, 1.5f };
                }
                if (type == NPCType<Atherian>())
                {
                    npc.ElementMultipliers() = new[] { 0.5f, 1.0f, 0.4f, 2.0f };
                }
                if (type == NPCType<CaveHarpy>() ||
                    type == NPCType<HallowedHarpy>())
                {
                    npc.ElementMultipliers() = new[] { 2.0f, 0.8f, 0.5f, 1.0f };
                }
                if (type == NPCType<SnowHarpy>())
                {
                    npc.ElementMultipliers() = new[] { 2.0f, 0.8f, 2.0f, 1.0f };
                }
                if (type == NPCType<AreusMortar>())
                {
                    npc.ElementMultipliers() = new[] { 0.8f, 1.0f, 0.8f, 2.0f };
                }
            }
        }
    }
}
