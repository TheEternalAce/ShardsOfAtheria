using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class CardinalSoulNPC : GlobalNPC
    {
        /// <summary>
        /// Hitting an enemy with a soul anathema doubles damage<br/>
        /// 0 - Should not be used<br/>
        /// 1 - Envy<br/>
        /// 2 - Gluttony<br/>
        /// 3 - Greed<br/>
        /// 4 - Lust<br/>
        /// 5 - Pride<br/>
        /// 6 - Sloth<br/>
        /// 7 - Wrath<br/>
        /// 8 - Charity<br/>
        /// 9 - Chassity<br/>
        /// 10 - Diligence<br/>
        /// 11 - Humility<br/>
        /// 12 - Kindness<br/>
        /// 13 - Patience<br/>
        /// 14 - Temperance
        /// </summary>
        public bool[] soulAnathema = new bool[15];
        /// <summary>
        /// Hitting an enemy with a soul edict halves damage<br/>
        /// 0 - Should not be used<br/>
        /// 1 - Envy<br/>
        /// 2 - Gluttony<br/>
        /// 3 - Greed<br/>
        /// 4 - Lust<br/>
        /// 5 - Pride<br/>
        /// 6 - Sloth<br/>
        /// 7 - Wrath<br/>
        /// 8 - Charity<br/>
        /// 9 - Chassity<br/>
        /// 10 - Diligence<br/>
        /// 11 - Humility<br/>
        /// 12 - Kindness<br/>
        /// 13 - Patience<br/>
        /// 14 - Temperance
        /// </summary>
        public bool[] soulEdict = new bool[15];

        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC entity)
        {
            int type = entity.type;
            if (EnviousNPCs.Contains(type)) entity.NPCSoul(CardinalSoulID.Envy);
            if (GluttonousNPCs.Contains(type)) entity.NPCSoul(CardinalSoulID.Gluttony);
            if (GreedyNPCs.Contains(type)) entity.NPCSoul(CardinalSoulID.Greed);
            if (PridefulNPCs.Contains(type)) entity.NPCSoul(CardinalSoulID.Pride);
            if (SlothfulNPCs.Contains(type)) entity.NPCSoul(CardinalSoulID.Sloth);
            if (WrathfulNPCs.Contains(type)) entity.NPCSoul(CardinalSoulID.Wrath);
            if (type == NPCID.Demon || type == NPCID.RedDevil) entity.NPCSoul(Main.rand.Next(8));
            if (type == NPCID.WallofFlesh || type == NPCID.WallofFleshEye)
            {
                for (int i = 1; i < 8; i++)
                {
                    soulAnathema[i] = true;
                }
                soulEdict[CardinalSoulID.Diligence] = true;
                soulEdict[CardinalSoulID.Humility] = true;
                soulEdict[CardinalSoulID.Kindness] = true;
            }
            if (type == NPCID.Golem || type == NPCID.GolemFistLeft || type == NPCID.GolemFistRight || type == NPCID.GolemHead)
            {
                soulEdict[CardinalSoulID.Pride] = true;
                soulEdict[CardinalSoulID.Humility] = true;
                soulEdict[CardinalSoulID.Patience] = true;
                soulEdict[CardinalSoulID.Temperance] = true;
            }
            if (type == NPCID.SkeletronPrime || type == NPCID.Retinazer || type == NPCID.CultistBoss || type == NPCID.PrimeCannon || type == NPCID.PrimeLaser || type == NPCID.PrimeSaw || type == NPCID.PrimeVice)
            {
                soulAnathema[CardinalSoulID.Patience] = false;
                soulEdict[CardinalSoulID.Patience] = true;
            }
            if (type == NPCID.Psycho || type == NPCID.Paladin)
            {
                soulEdict[CardinalSoulID.Kindness] = true;
                soulEdict[CardinalSoulID.Patience] = true;
            }
            if (type == NPCID.Paladin)
            {
                soulEdict[CardinalSoulID.Diligence] = true;
                soulEdict[CardinalSoulID.Temperance] = true;
                soulEdict[CardinalSoulID.Wrath] = true;
                soulAnathema[CardinalSoulID.Envy] = true;
                soulAnathema[CardinalSoulID.Lust] = true;
                soulAnathema[CardinalSoulID.Sloth] = true;
            }
        }

        static readonly List<int> EnviousNPCs = [
            NPCID.Eyezor,
            NPCID.Medusa,
            NPCID.Plantera,
            NPCID.PlanterasTentacle,
            NPCID.PrimeLaser,
            NPCID.Retinazer,
            NPCID.StardustJellyfishBig,
            NPCID.StardustCellBig,
            NPCID.StardustCellSmall,
            NPCID.StardustSoldier,
            NPCID.StardustSpiderBig,
            NPCID.StardustSpiderSmall,
            NPCID.StardustWormHead,
            NPCID.LunarTowerStardust,
        ];
        static readonly List<int> GluttonousNPCs = [
            NPCID.SandsharkCorrupt,
            NPCID.EaterofSouls,
            NPCID.EaterofWorldsBody,
            NPCID.EaterofWorldsHead,
            NPCID.EaterofWorldsTail,
            NPCID.DevourerBody,
            NPCID.DevourerHead,
            NPCID.DevourerTail,
            #region Slime
            NPCID.BabySlime,
            NPCID.BlackSlime,
            NPCID.BlueSlime,
            NPCID.DungeonSlime,
            NPCID.GreenSlime,
            NPCID.IceSlime,
            NPCID.JungleSlime,
            NPCID.KingSlime,
            NPCID.LavaSlime,
            NPCID.MotherSlime,
            NPCID.PurpleSlime,
            NPCID.QueenSlimeBoss,
            NPCID.QueenSlimeMinionBlue,
            NPCID.QueenSlimeMinionPink,
            NPCID.QueenSlimeMinionPurple,
            NPCID.RainbowSlime,
            NPCID.RedSlime,
            NPCID.SandSlime,
            NPCID.ShimmerSlime,
            NPCID.UmbrellaSlime,
            NPCID.YellowSlime,
            NPCID.SpikedIceSlime,
            NPCID.SpikedJungleSlime,
            NPCID.SlimeSpiked,
            NPCID.ToxicSludge,
            #endregion
            #region Zombie
            NPCID.ArmedTorchZombie,
            NPCID.ArmedZombie,
            NPCID.ArmedZombieCenx,
            NPCID.ArmedZombieEskimo,
            NPCID.ArmedZombiePincussion,
            NPCID.ArmedZombieSlimed,
            NPCID.ArmedZombieSwamp,
            NPCID.ArmedZombieTwiggy,
            NPCID.BloodZombie,
            NPCID.DoctorBones,
            NPCID.TheBride,
            NPCID.TheGroom,
            NPCID.Zombie,
            NPCID.ZombieDoctor,
            NPCID.ZombieElf,
            NPCID.ZombieElfBeard,
            NPCID.ZombieElfGirl,
            NPCID.ZombieEskimo,
            NPCID.ZombieMerman,
            NPCID.ZombieMushroom,
            NPCID.ZombieMushroomHat,
            NPCID.ZombiePixie,
            NPCID.ZombieRaincoat,
            NPCID.ZombieSuperman,
            NPCID.ZombieSweater,
            NPCID.ZombieXmas,
            #endregion
            NPCID.TheDestroyer,
            NPCID.TheDestroyerBody,
            NPCID.TheDestroyerTail,
            NPCID.VortexHornet,
            NPCID.VortexHornetQueen,
            NPCID.VortexLarva,
            NPCID.VortexRifleman,
            NPCID.VortexSoldier,
            NPCID.LunarTowerVortex,
            NPCID.TheHungry,
            NPCID.TheHungryII,
            NPCID.LeechBody,
            NPCID.LeechHead,
            NPCID.LeechTail,
            NPCID.Vampire,
            NPCID.VampireBat,
            NPCID.SeekerBody,
            NPCID.SeekerHead,
            NPCID.SeekerTail,
        ];
        static readonly List<int> GreedyNPCs = [
            NPCID.DD2Betsy,
            NPCID.BigMimicCorruption,
            NPCID.BigMimicCrimson,
            NPCID.BigMimicHallow,
            NPCID.BigMimicJungle,
            NPCID.IceMimic,
            NPCID.Mimic,
            NPCID.PresentMimic,
            NPCID.PrimeVice,
            NPCID.Pirate,
            NPCID.PirateCaptain,
            NPCID.PirateCorsair,
            NPCID.PirateCrossbower,
            NPCID.PirateDeadeye,
            NPCID.PirateDeckhand,
            NPCID.PirateGhost,
            NPCID.PirateShip,
            NPCID.PirateShipCannon,
        ];
        static readonly List<int> PridefulNPCs = [
            NPCID.AncientCultistSquidhead,
            NPCID.DD2Betsy,
            NPCID.BrainofCthulhu,
            NPCID.MoonLordCore,
            NPCID.MoonLordHand,
            NPCID.MoonLordHead,
            NPCID.GreekSkeleton,
            NPCID.HallowBoss,
            NPCID.SkeletronPrime,
            NPCID.Tim,
            NPCID.Unicorn,
            NPCID.NebulaBeast,
            NPCID.NebulaBrain,
            NPCID.NebulaHeadcrab,
            NPCID.NebulaSoldier,
            NPCID.LunarTowerNebula,
        ];
        static readonly List<int> SlothfulNPCs = [
            #region Zombie
            NPCID.ArmedTorchZombie,
            NPCID.ArmedZombie,
            NPCID.ArmedZombieCenx,
            NPCID.ArmedZombieEskimo,
            NPCID.ArmedZombiePincussion,
            NPCID.ArmedZombieSlimed,
            NPCID.ArmedZombieSwamp,
            NPCID.ArmedZombieTwiggy,
            NPCID.BloodZombie,
            NPCID.DoctorBones,
            NPCID.TheBride,
            NPCID.TheGroom,
            NPCID.Zombie,
            NPCID.ZombieDoctor,
            NPCID.ZombieElf,
            NPCID.ZombieElfBeard,
            NPCID.ZombieElfGirl,
            NPCID.ZombieEskimo,
            NPCID.ZombieMerman,
            NPCID.ZombieMushroom,
            NPCID.ZombieMushroomHat,
            NPCID.ZombiePixie,
            NPCID.ZombieRaincoat,
            NPCID.ZombieSuperman,
            NPCID.ZombieSweater,
            NPCID.ZombieXmas,
            #endregion
        ];
        static readonly List<int> WrathfulNPCs = [
            NPCID.AngryBones,
            NPCID.AngryBonesBig,
            NPCID.AngryBonesBigHelmet,
            NPCID.AngryBonesBigMuscle,
            NPCID.AngryNimbus,
            NPCID.AngryTrapper,
            NPCID.BloodCrawler,
            NPCID.BloodCrawlerWall,
            NPCID.BloodEelBody,
            NPCID.BloodEelHead,
            NPCID.BloodEelTail,
            NPCID.BloodFeeder,
            NPCID.BloodJelly,
            NPCID.BloodMummy,
            NPCID.BloodNautilus,
            NPCID.BloodSquid,
            NPCID.BloodZombie,
            NPCID.BrainofCthulhu,
            NPCID.Butcher,
            NPCID.Dandelion,
            NPCID.EyeofCthulhu,
            NPCID.SandsharkCrimson,
            NPCID.Frankenstein,
            NPCID.Ghost,
            NPCID.PirateGhost,
            NPCID.CultistDragonBody1,
            NPCID.CultistDragonBody2,
            NPCID.CultistDragonBody3,
            NPCID.CultistDragonBody4,
            NPCID.CultistDragonHead,
            NPCID.CultistDragonTail,
            NPCID.PrimeSaw,
            NPCID.Reaper,
            NPCID.SolarCorite,
            NPCID.SolarCrawltipedeTail,
            NPCID.SolarDrakomire,
            NPCID.SolarDrakomireRider,
            NPCID.SolarSolenian,
            NPCID.SolarSpearman,
            NPCID.SolarSroller,
            NPCID.LunarTowerSolar,
            NPCID.Spazmatism,
        ];

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.CardinalSoul().SlothfulSinner)
                spawnRate = (int)(spawnRate * 0.5f);
        }
    }
}
