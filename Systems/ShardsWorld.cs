using ShardsOfAtheria.Items.Placeable.Furniture;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria.Tiles.Furniture;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ShardsOfAtheria.Systems
{
    public class ShardsWorld : ModSystem
    {
        public bool omegaKey;
        public bool omegaShrine;
        public bool underworldDecaShrine;

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                // Next, we insert our step directly after the original "Shinies" step. 
                // ExampleModOres is a method seen below.
                tasks.Insert(ShiniesIndex + 1, new SoAOres("Shards of Atheria Ores", 237.4298f));
            }

            int ShiniesIndex1 = tasks.FindIndex(genpass => genpass.Name.Equals("Random Gems"));

            if (ShiniesIndex1 != -1)
            {
                tasks.Insert(ShiniesIndex1 + 1, new UnderworldShrinePass("UnderworldShrine", 237.4298f));
            }
        }

        public override void PostWorldGen()
        {
            for (int k = 0; k < Main.maxChests; k++)
            {
                Chest c = Main.chest[k];
                if (c != null)
                {
                    if (Main.tile[c.x, c.y].TileType == TileID.Containers)
                    {
                        int style = ChestTypes.GetChestStyle(c);
                        if (style == ChestTypes.Skyware)
                        {
                            if (!omegaKey && Main.rand.NextBool(14))
                            {
                                c.Insert(ModContent.ItemType<OmegaKey>(), 1);
                                omegaKey = true;
                            }
                        }
                        else if (style == ChestTypes.LockedGold)
                        {
                            //c.Insert(ModContent.ItemType<>(), 1);
                        }
                    }
                }
            }
            base.PostWorldGen();
        }
    }

    public class SoAOres : GenPass
    {
        public SoAOres(string name, float loadWeight) : base(name, loadWeight)
        {

        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Shards Of Atheria Ores";
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<AreusOre>());
            }
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<BionicOre>());
            }
        }
    }

    public class UnderworldShrinePass : GenPass
    {
        public UnderworldShrinePass(string name, float loadWeight) : base(name, loadWeight)
        {

        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating shrines";
            // attempts to spawn shrine 1000 times, so hopefully it actually will generate
            for (int t = 0; t < 1000; t++)
            {
                int x = WorldGen.genRand.Next(Main.maxTilesX);

                // Fuck you it can also spawn on the right now

                if (x < Main.maxTilesX * 0.2f && x > Main.maxTilesX * 0.05f || x > Main.maxTilesX * 0.8f && x < Main.maxTilesX * 0.95f)
                {
                    int y = WorldGen.genRand.Next((int)(Main.maxTilesY * 0.85f), Main.maxTilesY);

                    if (Main.tile[x, y].TileType == TileID.Ash && !Main.tile[x, y - 1].HasTile && Main.tile[x, y - 1].LiquidType != LiquidID.Lava)
                    {
                        if (!ModContent.GetInstance<ShardsWorld>().omegaShrine)
                        {
                            PlaceOmegaShrine(x, y);
                            ModContent.GetInstance<ShardsWorld>().omegaShrine = true;
                        }
                    }
                }
            }
        }

        private readonly int[,] _omegashrineshape = {
            {0,3,0,0,2,2,0,0,3,0},
            {0,3,3,2,2,2,2,3,3,0},
            {5,3,3,3,2,2,3,3,3,5},
            {5,5,3,3,5,5,3,3,5,5},
            {5,5,5,5,5,5,5,5,5,5},
            {5,5,5,5,4,5,5,5,5,5},
            {1,1,5,5,0,0,5,5,1,1},
            {0,1,1,2,2,2,2,1,1,0},
        };
        private readonly int[,] _omegashrinechestshape = {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,1,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
        };
        private readonly int[,] _omegashrineslopeshape = {
            {0,2,0,0,1,1,0,0,3,0},
            {0,0,2,3,0,0,2,3,0,0},
            {0,4,0,0,4,5,0,0,5,0},
            {0,0,4,0,0,0,0,5,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {4,2,0,0,0,0,0,0,3,5},
            {0,4,0,0,0,0,0,0,5,0},
        };
        private readonly int[,] _omegashrinewallshape = {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,2,1,1,1,1,2,0,0},
            {0,0,2,1,3,3,1,2,0,0},
            {0,0,2,1,1,1,1,2,0,0},
            {0,0,2,1,1,1,1,2,0,0},
            {0,0,2,1,1,1,1,2,0,0},
            {0,0,0,0,0,0,0,0,0,0},
        };

        public bool PlaceOmegaShrine(int i, int j)
        {
            for (int y = 0; y < _omegashrineshape.GetLength(0); y++)
            {
                for (int x = 0; x < _omegashrineshape.GetLength(1); x++)
                {
                    int k = i - 5 + x;
                    int l = j - 4 + y;

                    int tileType = 0;

                    SlopeType slopeType = 0;
                    switch (_omegashrineshape[y, x])
                    {
                        case 1:
                            tileType = TileID.PlatinumBrick;
                            break;
                        case 2:
                            tileType = TileID.Titanstone;
                            break;
                        case 3:
                            tileType = TileID.AdamantiteBeam;
                            break;
                        case 4:
                            // switch this to modded chest
                            //	WorldGen.PlaceChestDirect(k, l, 21, 0, 1);
                            //	WorldGen.PlaceChest(k, l); //, 21, false, 3);
                            break;
                    }
                    switch (_omegashrineslopeshape[y, x])
                    {
                        case 1:
                            slopeType = SlopeType.Solid;
                            break;
                        case 2:
                            slopeType = SlopeType.SlopeDownLeft;
                            break;
                        case 3:
                            slopeType = SlopeType.SlopeDownRight;
                            break;
                        case 4:
                            slopeType = SlopeType.SlopeUpRight;
                            break;
                        case 5:
                            slopeType = SlopeType.SlopeUpLeft;
                            break;
                    }
                    switch (_omegashrinewallshape[y, x])
                    {
                        case 1:
                            WorldGen.PlaceWall(k, l, WallID.TitanstoneBlock);
                            break;
                        case 2:
                            WorldGen.PlaceWall(k, l, WallID.AdamantiteBeam);
                            break;
                        case 3:
                            WorldGen.PlaceWall(k, l, WallID.AmethystGemspark);
                            break;
                    }

                    if (tileType == 5)
                    {
                        // removes any existing tiles so it looks better
                        Main.tile[k, l].ClearTile();
                    }

                    if (tileType != 0 && tileType != 5)
                    {
                        // removes any existing tiles so it looks better
                        Main.tile[k, l].ClearTile();

                        WorldGen.PlaceTile(k, l, tileType);
                        Tile tile = Framing.GetTileSafely(k, l);
                        tile.Slope = slopeType;
                        if (_omegashrineslopeshape[y, x] == 1)
                        {
                            tile.IsHalfBlock = true;
                        }
                    }
                }
            }
            // Chest spawn and loot
            // Has to be seperate otherwise it doesnt spawn properly
            for (int y = 0; y < _omegashrineshape.GetLength(0); y++)
            {
                for (int x = 0; x < _omegashrineshape.GetLength(1); x++)
                {
                    int k = i - 5 + x;
                    int l = j - 4 + y;

                    switch (_omegashrinechestshape[y, x])
                    {
                        case 1:
                            // switch this to modded chest
                            int chestindex = WorldGen.PlaceChest(k, l + 1, (ushort)ModContent.TileType<OmegaChest>(), false, 1);
                            if (chestindex >= 0)
                            {
                                Chest chest = Main.chest[chestindex];
                                for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                                {
                                    if (chest.item[inventoryIndex].type == ItemID.None)
                                    {
                                        chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<TheMessiah>());
                                        break;
                                    }
                                }
                            }
                            break;
                    }

                }
            }
            return true;
        }
    }
}
