using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using SagesMania.Tiles;
using Terraria.ModLoader.IO;

namespace SagesMania
{
    class SMWorld : ModWorld
	{
		public bool flightToggle;

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
		public bool blueprints;
		public int message;

		public override TagCompound Save()
        {
			return new TagCompound
			{
				{"flightToggle", flightToggle},

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
				{"blueprints", blueprints},
				{"message", message},
			};
        }

        public override void Load(TagCompound tag)
		{
			flightToggle = tag.GetBool("flightToggle");

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
			blueprints = tag.GetBool("blueprints");
			message = tag.GetInt("message");
		}

        public override void PostUpdate()
		{
			if (slainValkyrie && slainEOC && (slainBOC || slainEOW) && slainBee && slainSkull && slainWall
				&& slainMechWorm && slainTwins && slainPrime && slainPlant && slainGolem && slainMoonLord)
				slainEverything = true;

			if (slayerMode && slainEverything && message <= 960)
            {
				message++;
				if (message == 120)
					Main.NewText("I see...");
				if (message == 240)
					Main.NewText("So, you've slain every guardian I have..");
				if (message == 480)
					Main.NewText("Well, Come then, face me Soulless Terrarian.");
				if (message == 960)
					Main.NewText("I'll be waiting.");
			}

			if (slainSenterra && !slainGenesis)
				Main.dayTime = false;
		}

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                // Next, we insert our step directly after the original "Shinies" step. 
                // ExampleModOres is a method seen below.
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Sage's Mania Ores", SMOres));
            }
        }

		private void SMOres(GenerationProgress progress)
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
