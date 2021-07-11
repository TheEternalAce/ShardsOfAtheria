using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.World.Generation;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using SagesMania.Tiles;
using Terraria.ModLoader.IO;

namespace SagesMania
{
    class SMWorld : ModWorld
	{
		public bool slayerMode;
		public bool slainEOC;
		public bool slainBOC;
		public bool slainEOW;
		public bool slainBee;
		public bool slainSkull;
		public bool slainWall;

        public override TagCompound Save()
        {
			return new TagCompound
			{
				{"slayerMode", slayerMode},
				{"slainEOC", slainEOC},
				{"slainBOC", slainBOC},
				{"slainEOW", slainEOW},
				{"slainBee", slainBee},
				{"slainSkull", slainSkull},
				{"slainWall", slainWall},
			};
        }

        public override void Load(TagCompound tag)
        {
			slayerMode = tag.GetBool("slayerMode");
			slainEOC = tag.GetBool("slainEOC");
			slainBOC = tag.GetBool("slainBOC");
			slainEOW = tag.GetBool("slainEOW");
			slainBee = tag.GetBool("slainBee");
			slainSkull = tag.GetBool("slainSkull");
			slainWall = tag.GetBool("slainWall");
        }

        public override void PostUpdate()
        {
			if (NPC.downedBoss1 && slayerMode)
				slainEOC = true;
			if (NPC.downedBoss2 && slayerMode)
				slainBOC = true;
			if (NPC.downedBoss2 && slayerMode)
				slainEOW = true;
			if (NPC.downedQueenBee && slayerMode)
				slainBee = true;
			if (NPC.downedBoss3 && slayerMode)
				slainSkull = true;
			if (Main.hardMode && slayerMode)
				slainWall = true;
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
