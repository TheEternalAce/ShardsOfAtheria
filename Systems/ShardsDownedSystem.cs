using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Systems
{
    public class ShardsDownedSystem : ModSystem
    {
        public static bool downedDeath = false;
        public static bool downedValkyrie = false;
        public static bool downedSenterra = false;
        public static bool downedGenesis = false;

        public bool slainDeath = false;
        public bool slainKing = false;
        public bool slainEOC = false;
        public bool slainBOC = false;
        public bool slainEOW = false;
        public bool slainValkyrie = false;
        public bool slainAtherian = false;
        public bool slainBee = false;
        public bool slainSkull = false;
        public bool slainDeerclops = false;
        public bool slainWall = false;
        public bool slainQueen = false;
        public bool slainMechWorm = false;
        public bool slainTwins = false;
        public bool slainPrime = false;
        public bool slainPlant = false;
        public bool slainGolem = false;
        public bool slainDuke = false;
        public bool slainEmpress = false;
        public bool slainLunatic = false;
        public bool slainPillarNebula = false;
        public bool slainPillarSolar = false;
        public bool slainPillarStardust = false;
        public bool slainPillarVortex = false;
        public bool slainMoonLord = false;
        public bool slainSenterra = false;
        public bool slainGenesis = false;

        public static List<int> slainBosses = new();

        public override void OnWorldUnload()
        {
            downedDeath = false;
            downedValkyrie = false;
            downedSenterra = false;
            downedGenesis = false;

            slainKing = false;
            slainEOC = false;
            slainBOC = false;
            slainEOW = false;
            slainBee = false;
            slainSkull = false;
            slainValkyrie = false;
            slainAtherian = false;
            slainDeerclops = false;
            slainWall = false;
            slainQueen = false;
            slainMechWorm = false;
            slainTwins = false;
            slainPrime = false;
            slainPlant = false;
            slainGolem = false;
            slainDuke = false;
            slainEmpress = false;
            slainLunatic = false;
            slainMoonLord = false;
            slainDeath = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["downedDeath"] = downedDeath;
            tag["downedValkyrie"] = downedValkyrie;
            tag["downedDeath"] = downedDeath;
            tag["downedValkyrie"] = downedValkyrie;

            tag["slainKing"] = slainKing;
            tag["slainEOC"] = slainEOC;
            tag["slainBOC"] = slainBOC;
            tag["slainEOW"] = slainEOW;
            tag["slainValkyrie"] = slainValkyrie;
            tag["slainAtherian"] = slainAtherian;
            tag["slainBee"] = slainBee;
            tag["slainSkull"] = slainSkull;
            tag["slainDeerclops"] = slainDeerclops;
            tag["slainWall"] = slainWall;
            tag["slainQueen"] = slainQueen;
            tag["slainMechWorm"] = slainMechWorm;
            tag["slainTwins"] = slainTwins;
            tag["slainPrime"] = slainPrime;
            tag["slainPlant"] = slainPlant;
            tag["slainGolem"] = slainGolem;
            tag["slainDuke"] = slainDuke;
            tag["slainEmpress"] = slainEmpress;
            tag["slainLunatic"] = slainLunatic;
            tag["slainMoonLord"] = slainMoonLord;
            tag["slainDeath"] = slainDeath;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("downedValkyrie"))
                downedValkyrie = tag.GetBool("downedValkyrie");
            if (tag.ContainsKey("downedDeath"))
                downedDeath = tag.GetBool("downedDeath");
            if (tag.ContainsKey("downedSenterra"))
                downedSenterra = tag.GetBool("downedSenterra");
            if (tag.ContainsKey("downedGenesis"))
                downedGenesis = tag.GetBool("downedGenesis");

            if (tag.ContainsKey("slainKing"))
                slainKing = tag.GetBool("slainKing");
            if (tag.ContainsKey("slainEOC"))
                slainEOC = tag.GetBool("slainEOC");
            if (tag.ContainsKey("slainBOC"))
                slainBOC = tag.GetBool("slainBOC");
            if (tag.ContainsKey("slainEOW"))
                slainEOW = tag.GetBool("slainEOW");
            if (tag.ContainsKey("slainValkyrie"))
                slainValkyrie = tag.GetBool("slainValkyrie");
            if (tag.ContainsKey("slainAtherian"))
                slainAtherian = tag.GetBool("slainAtherian");
            if (tag.ContainsKey("slainBee"))
                slainBee = tag.GetBool("slainBee");
            if (tag.ContainsKey("slainSkull"))
                slainSkull = tag.GetBool("slainSkull");
            if (tag.ContainsKey("slainWall"))
                slainWall = tag.GetBool("slainWall");
            if (tag.ContainsKey("GetBool"))
                slainMechWorm = tag.ContainsKey("slainMechWorm");
            if (tag.ContainsKey("slainTwins"))
                slainTwins = tag.GetBool("slainTwins");
            if (tag.ContainsKey("slainPrime"))
                slainPrime = tag.GetBool("slainPrime");
            if (tag.ContainsKey("slainPlant"))
                slainPlant = tag.GetBool("slainPlant");
            if (tag.ContainsKey("slainGolem"))
                slainGolem = tag.GetBool("slainGolem");
            if (tag.ContainsKey("slainDuke"))
                slainDuke = tag.GetBool("slainDuke");
            if (tag.ContainsKey("slainEmpress"))
                slainEmpress = tag.GetBool("slainEmpress");
            if (tag.ContainsKey("slainLunatic"))
                slainLunatic = tag.GetBool("slainLunatic");
            if (tag.ContainsKey("slainMoonLord"))
                slainMoonLord = tag.GetBool("slainMoonLord");
            if (tag.ContainsKey("slainDeath"))
                slainDeath = tag.GetBool("slainDeath");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedValkyrie;
            flags[1] = downedDeath;

            flags[2] = slainKing;
            flags[3] = slainEOC;
            flags[4] = slainBOC;
            flags[5] = slainEOW;
            flags[6] = slainValkyrie;
            flags[7] = slainBee;
            writer.Write(flags);

            BitsByte flags2 = new BitsByte();
            flags2[0] = slainSkull;
            flags2[1] = slainDeerclops;
            flags2[2] = slainWall;
            flags2[3] = slainDeerclops;
            flags2[4] = slainMechWorm;
            flags2[5] = slainTwins;
            flags2[6] = slainPrime;
            flags2[7] = slainPlant;
            writer.Write(flags2);

            BitsByte flags3 = new BitsByte();
            flags3[0] = slainGolem;
            flags3[1] = slainDuke;
            flags3[2] = slainEmpress;
            flags3[3] = slainLunatic;
            flags3[4] = slainMoonLord;
            flags3[5] = slainDeath;
            flags3[6] = slainGenesis;
            flags3[6] = slainSenterra;
            writer.Write(flags3);

            BitsByte flags4 = new BitsByte();
            flags4[0] = downedSenterra;
            flags4[1] = downedGenesis;
            flags4[1] = slainAtherian;
            writer.Write(flags4);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();

            downedValkyrie = flags[0];
            downedDeath = flags[1];

            slainKing = flags[2];
            slainEOC = flags[3];
            slainBOC = flags[4];
            slainEOW = flags[5];
            slainValkyrie = flags[6];
            slainBee = flags[7];

            BitsByte flags2 = reader.ReadByte();
            slainSkull = flags2[0];
            slainDeerclops = flags2[1];
            slainWall = flags2[2];
            slainQueen = flags2[3];
            slainMechWorm = flags2[4];
            slainTwins = flags2[5];
            slainPrime = flags2[6];
            slainPlant = flags2[7];

            BitsByte flags3 = reader.ReadByte();
            slainGolem = flags3[0];
            slainDuke = flags3[1];
            slainEmpress = flags3[2];
            slainLunatic = flags3[3];
            slainMoonLord = flags3[4];
            slainDeath = flags3[5];
            slainSenterra = flags3[6];
            slainGenesis = flags3[7];

            BitsByte flags4 = reader.ReadByte();
            downedSenterra = flags4[0];
            downedGenesis = flags4[1];
            slainAtherian = flags4[1];

        }

        public override void PostUpdateEverything()
        {
            if (slainSenterra && !slainGenesis)
            {
                Main.dayTime = false;
                Main.time = 4 * 3600 + 30 * 60;
            }
        }
    }
}
