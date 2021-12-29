using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using ShardsOfAtheria.Tiles;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace ShardsOfAtheria
{
    class SMWorld : ModSystem
	{
		public bool flightDisabled;

		public bool downedDeath;
		public bool downedValkyrie;
		public bool slayerMode;
		public bool slainDeath;
		public bool slainValkyrie;
		public bool slainEOC;
		public bool slainBOC;
		public bool slainEOW;
		public bool slainBee;
		public bool slainSkull;
		public bool slainWall;
		public bool slainMechWorm;
		public bool slainTwins;
		public bool slainPrime;
		public bool slainPlant;
		public bool slainGolem;
		public bool slainDuke;
		public bool slainEmpress;
		public bool slainMoonLord;
		public bool slainSenterra;
		public bool slainGenesis;
		public bool slainEverything;

        public override void SaveWorldData(TagCompound tag)
        {
			new TagCompound
			{
				{"flightDisabled", flightDisabled},

				{"downedDeath", downedDeath},
				{"downedValkyrie", downedValkyrie},
				{"slayerMode", slayerMode},
				{"slainDeath", slainDeath},
				{"slainValkyrie", slainValkyrie},
				{"slainEOC", slainEOC},
				{"slainBOC", slainBOC},
				{"slainEOW", slainEOW},
				{"slainBee", slainBee},
				{"slainSkull", slainSkull},
				{"slainWall", slainWall},
				{"slainMechWorm", slainMechWorm},
				{"slainTwins", slainTwins},
				{"slainPrime", slainPrime},
				{"slainPlant", slainPlant},
				{"slainGolem", slainGolem},
				{"slainDuke", slainDuke},
				{"slainEmpress", slainEmpress},
				{"slainMoonLord", slainMoonLord},
				{"slainEverything", slainEverything},
			};
		}

        public override void LoadWorldData(TagCompound tag)
		{
			flightDisabled = tag.GetBool("flightDisabled");

			downedDeath = tag.GetBool("downedDeath");
			downedValkyrie = tag.GetBool("downedValkyrie");
			slayerMode = tag.GetBool("slayerMode");
			slainValkyrie = tag.GetBool("slainValkyrie");
			slainDeath = tag.GetBool("slainDeath");
			slainEOC = tag.GetBool("slainEOC");
			slainBOC = tag.GetBool("slainBOC");
			slainEOW = tag.GetBool("slainEOW");
			slainBee = tag.GetBool("slainBee");
			slainSkull = tag.GetBool("slainSkull");
			slainWall = tag.GetBool("slainWall");
			slainMechWorm = tag.GetBool("slainMechWorm");
			slainTwins = tag.GetBool("slainTwins");
			slainPrime = tag.GetBool("slainPrime");
			slainPlant = tag.GetBool("slainPlant");
			slainGolem = tag.GetBool("slainGolem");
			slainDuke = tag.GetBool("slainDuke");
			slainEmpress = tag.GetBool("slainEmpress");
			slainMoonLord = tag.GetBool("slainMoonLord");
			slainEverything = tag.GetBool("slainEverything");
		}

        public override void PostUpdateEverything()
		{
			if (slainValkyrie && slainEOC && (slainBOC || slainEOW) && slainBee && slainSkull && slainWall
				&& slainMechWorm && slainTwins && slainPrime && slainPlant && slainGolem && slainMoonLord)
				slainEverything = true;

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
			progress.Message = "Ore";
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
			progress.Message = "Spreading Infection";
			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<InfectionCrystal>());
			}
			progress.Message = "Causing Errors";
			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY); // WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<GlitchTile>());
			}
		}
    }
}
