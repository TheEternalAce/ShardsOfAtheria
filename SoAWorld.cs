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
		public static bool downedDeath;
		public static bool downedValkyrie;
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

		public int messageToPlayer;

		public override void OnWorldLoad()
		{
			slayerMode = false;

			downedDeath = false;
			downedValkyrie = false;

			slainDeath = false;
			slainValkyrie = false;
			slainEOC = false;
			slainBOC = false;
			slainEOW = false;
			slainBee = false;
			slainSkull = false;
			slainWall = false;
			slainMechWorm = false;
			slainTwins = false;
			slainPrime = false;
			slainPlant = false;
			slainGolem = false;
			slainDuke = false;
			slainEmpress = false;
			slainMoonLord = false;
			slainEOC = false;

			messageToPlayer = 0;
		}

		public override void OnWorldUnload()
		{
			slayerMode = false;

			downedDeath = false;
			downedValkyrie = false;

			slainDeath = false;
			slainValkyrie = false;
			slainEOC = false;
			slainBOC = false;
			slainEOW = false;
			slainBee = false;
			slainSkull = false;
			slainWall = false;
			slainMechWorm = false;
			slainTwins = false;
			slainPrime = false;
			slainPlant = false;
			slainGolem = false;
			slainDuke = false;
			slainEmpress = false;
			slainMoonLord = false;
			slainEOC = false;

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
			if (slainValkyrie)
				tag["slainValkyrie"] = true;
			if (slainEOC)
				tag["slainEOC"] = true;
			if (slainBOC)
				tag["slainBOC"] = true;
			if (slainEOW)
				tag["slainEOW"] = true;
			if (slainBee)
				tag["slainBee"] = true;
			if (slainSkull)
				tag["slainSkull"] = true;
			if (slainWall)
				tag["slainWall"] = true;
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
			if (slainMoonLord)
				tag["slainMoonLord"] = true;

			tag["messageToPlayer"] = messageToPlayer;
		}

        public override void LoadWorldData(TagCompound tag)
		{
			slayerMode = tag.ContainsKey("slayerMode");

			downedDeath = tag.ContainsKey("downedDeath");
			downedValkyrie = tag.ContainsKey("downedValkyrie");
			slainValkyrie = tag.ContainsKey("slainValkyrie");
			slainDeath = tag.ContainsKey("slainDeath");
			slainEOC = tag.ContainsKey("slainEOC");
			slainBOC = tag.ContainsKey("slainBOC");
			slainEOW = tag.ContainsKey("slainEOW");
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
			slainMoonLord = tag.ContainsKey("slainMoonLord");
			slainEverything = tag.ContainsKey("slainEverything");

			messageToPlayer = tag.GetInt("messageToPlayer");
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = slayerMode;

			flags[1] = downedDeath;
			flags[2] = downedValkyrie;

			flags[3] = slainDeath;
			flags[4] = slainValkyrie;
			flags[5] = slainEOC;
			flags[6] = slainBOC;
			flags[7] = slainEOW;
			writer.Write(flags);

			BitsByte flags2 = new BitsByte();
			flags2[0] = slainBee;
			flags2[1] = slainSkull;
			flags2[2] = slainWall;
			flags2[3] = slainMechWorm;
			flags2[4] = slainTwins;
			flags2[5] = slainPrime;
			flags2[6] = slainPlant;
			flags2[7] = slainGolem;
			writer.Write(flags2);

			BitsByte flags3 = new BitsByte();
			flags2[0] = slainDuke;
			flags2[1] = slainEmpress;
			flags3[2] = slainMoonLord;
			flags3[3] = slainEverything;
			writer.Write(flags3);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			slayerMode = flags[0];

			downedDeath = flags[1];
			downedValkyrie = flags[2];

			slainDeath = flags[3];
			slainValkyrie = flags[4];
			slainEOC = flags[5];
			slainBOC = flags[6];
			slainEOW = flags[7];

			BitsByte flags2 = reader.ReadByte();
			slainBee = flags2[0];
			slainSkull = flags2[1];
			slainWall = flags2[2];
			slainMechWorm = flags2[3];
			slainTwins = flags2[4];
			slainPrime = flags2[5];
			slainPlant = flags2[6];
			slainGolem = flags2[7];

			BitsByte flags3 = new BitsByte();
			slainDuke = flags3[0];
			slainEmpress = flags3[1];
			slainMoonLord = flags3[2];
			slainEverything = flags3[3];
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

			if (slainEverything)
            {
				messageToPlayer++;
				if (messageToPlayer == 240)
					Main.NewText("[c/FF00DA:Greetings.]");
				if (messageToPlayer == 360)
					Main.NewText("[c/FF00DA:I must say, I am impressed.]");
				if (messageToPlayer == 480)
					Main.NewText("[c/FF00DA:You've managed to get hold of my emblem and kill them all.]");
				if (messageToPlayer == 600)
					Main.NewText("[c/FF00DA:Who'd have known that their death would break the spell my sister put on me?]");
				if (messageToPlayer == 720)
					Main.NewText("[c/FF00DA:Regardless, you made yourself a perfect vessel.]");
				if (messageToPlayer == 840)
					Main.NewText("[c/FF00DA:You wouldn't mind if my consciousness took over yours would you?]");
				if (messageToPlayer == 960)
					Main.NewText("[c/FF00DA:Of course not]");
				if (messageToPlayer == 1080)
					Main.NewText("[c/FF0000:You don't have a choice.]");
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
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<InfectionCrystal>());
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
