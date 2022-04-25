using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace ShardsOfAtheria
{
    public class SoAWorld : ModSystem
	{
		public static bool downedDeath = false;
		public static bool downedValkyrie = false;
		public bool slayerMode = false;
		public bool slainDeath = false;
		public bool slainKing = false;
		public bool slainEOC = false;
		public bool slainBOC = false;
		public bool slainEOW = false;
		public bool slainValkyrie = false;
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
		public bool slainEverything = false;

		public int messageToPlayer = 0;

		public override void OnWorldUnload()
		{
			slayerMode = false;

			downedDeath = false;
			downedValkyrie = false;

			slainDeath = false;
			slainValkyrie = false;
			slainKing = false;
			slainEOC = false;
			slainBOC = false;
			slainEOW = false;
			slainBee = false;
			slainSkull = false;
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

			messageToPlayer = 0;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			if (slayerMode)
				tag["slayerMode"] = true;

			if (downedDeath)
				tag["downedDeath"] = true;
			if (downedValkyrie)
				tag["downedValkyrie"] = true;

			if (slainDeath)
				tag["slainDeath"] = true;
			if (slainKing)
				tag["slainKing"] = true;
			if (slainEOC)
				tag["slainEOC"] = true;
			if (slainBOC)
				tag["slainBOC"] = true;
			if (slainEOW)
				tag["slainEOW"] = true;
			if (slainValkyrie)
				tag["slainValkyrie"] = true;
			if (slainBee)
				tag["slainBee"] = true;
			if (slainSkull)
				tag["slainSkull"] = true;
			if (slainDeerclops)
				tag["slainDeerclops"] = true;
			if (slainWall)
				tag["slainWall"] = true;
			if (slainQueen)
				tag["slainQueen"] = true;
			if (slainMechWorm)
				tag["slainMechWorm"] = true;
			if (slainTwins)
				tag["slainTwins"] = true;
			if (slainPrime)
				tag["slainPrime"] = true;
			if (slainPlant)
				tag["slainPlant"] = true;
			if (slainGolem)
				tag["slainGolem"] = true;
			if (slainDuke)
				tag["slainDuke"] = true;
			if (slainEmpress)
				tag["slainEmpress"] = true;
			if (slainLunatic)
				tag["slainLunatic"] = true;
			if (slainMoonLord)
				tag["slainMoonLord"] = true;

			tag["messageToPlayer"] = messageToPlayer;
		}

        public override void LoadWorldData(TagCompound tag)
		{
			slayerMode = tag.ContainsKey("slayerMode");

			downedValkyrie = tag.ContainsKey("downedValkyrie");
			downedDeath = tag.ContainsKey("downedDeath");

			slainEOC = tag.ContainsKey("slainEOC");
			slainBOC = tag.ContainsKey("slainBOC");
			slainEOW = tag.ContainsKey("slainEOW");
			slainValkyrie = tag.ContainsKey("slainValkyrie");
			slainBee = tag.ContainsKey("slainBee");
			slainSkull = tag.ContainsKey("slainSkull");
			slainWall = tag.ContainsKey("slainWall");
			slainMechWorm = tag.ContainsKey("slainMechWorm");
			slainTwins = tag.ContainsKey("slainTwins");
			slainPrime = tag.ContainsKey("slainPrime");
			slainPlant = tag.ContainsKey("slainPlant");
			slainGolem = tag.ContainsKey("slainGolem");
			slainDuke = tag.ContainsKey("slainDuke");
			slainEmpress = tag.ContainsKey("slainEmpress");
			slainLunatic = tag.ContainsKey("slainLunatic");
			slainMoonLord = tag.ContainsKey("slainMoonLord");
			slainDeath = tag.ContainsKey("slainDeath");

			slainEverything = tag.ContainsKey("slainEverything");

			messageToPlayer = tag.GetInt("messageToPlayer");
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = slayerMode;

			flags[1] = downedValkyrie;
			flags[2] = downedDeath;

			flags[3] = slainKing;
			flags[4] = slainEOC;
			flags[5] = slainBOC;
			flags[6] = slainEOW;
			flags[7] = slainValkyrie;
			writer.Write(flags);

			BitsByte flags2 = new BitsByte();
			flags2[0] = slainBee;
			flags2[1] = slainSkull;
			flags2[2] = slainDeerclops;
			flags2[3] = slainWall;
			flags2[4] = slainDeerclops;
			flags2[5] = slainMechWorm;
			flags2[6] = slainTwins;
			flags2[7] = slainPrime;
			writer.Write(flags2);

			BitsByte flags3 = new BitsByte();
			flags3[0] = slainPlant;
			flags3[1] = slainGolem;
			flags3[2] = slainDuke;
			flags3[3] = slainEmpress;
			flags3[4] = slainLunatic;
			flags3[5] = slainMoonLord;
			flags3[6] = slainDeath;
			flags3[7] = slainGenesis;

			BitsByte flags4 = new BitsByte();
			flags4[0] = slainGenesis;

			flags4[1] = slainEverything;
			writer.Write(flags4);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			slayerMode = flags[0];

			downedValkyrie = flags[1];
			downedDeath = flags[2];

			slainKing = flags[3];
			slainEOC = flags[4];
			slainBOC = flags[5];
			slainEOW = flags[6];
			slainValkyrie = flags[7];

			BitsByte flags2 = reader.ReadByte();
			slainBee = flags2[0];
			slainSkull = flags2[1];
			slainDeerclops = flags2[2];
			slainWall = flags2[3];
			slainQueen = flags2[4];
			slainMechWorm = flags2[5];
			slainTwins = flags2[6];
			slainPrime = flags2[7];

			BitsByte flags3 = reader.ReadByte();
			slainPlant = flags3[0];
			slainGolem = flags3[1];
			slainDuke = flags3[2];
			slainEmpress = flags3[3];
			slainLunatic = flags3[4];
			slainMoonLord = flags3[5];
			slainDeath = flags3[6];
			slainGenesis = flags3[7];

			BitsByte flags4 = reader.ReadByte();
			slainSenterra = flags4[0];

			slainEverything = flags4[1];
		}

		public override void PostUpdateEverything()
		{
			if (slainSenterra && !slainGenesis)
			{
				Main.dayTime = false;
			}
		}

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                // Next, we insert our step directly after the original "Shinies" step. 
                // ExampleModOres is a method seen below.
                tasks.Insert(ShiniesIndex + 1, new SoAOres("Shards of Atheria Ores", 237.4298f));
            }
        }
	}

	public class SoAOres : GenPass
    {
		public SoAOres(string name, float loadWeight) : base(name, loadWeight) {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
			progress.Message = "Shards Of Atheria Ores";
			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<AreusOre>());
			}
			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<BionicOre>());
			}
			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<UraniumOreTile>());
			}
		}
    }
}