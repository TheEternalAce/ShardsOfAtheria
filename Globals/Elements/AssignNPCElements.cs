using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals.Elements
{
    public class AssignNPCElements : GlobalNPC
    {
        List<int> FireNPC = SoAGlobalNPC.FireNPC;
        List<int> IceNPC = SoAGlobalNPC.IceNPC;
        List<int> ElectricNPC = SoAGlobalNPC.ElectricNPC;
        List<int> MetalNPC = SoAGlobalNPC.MetalNPC;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            int type = npc.type;
            switch (type)
            {
                case NPCID.BlazingWheel:
                case NPCID.BoneSerpentHead:
                case NPCID.BoneSerpentBody:
                case NPCID.BoneSerpentTail:
                case NPCID.LavaSlime:
                case NPCID.Demon:
                case NPCID.FireImp:
                case NPCID.Hellbat:
                case NPCID.VoodooDemon:
                case NPCID.DiabolistRed:
                case NPCID.DiabolistWhite:
                case NPCID.HoppinJack:
                case NPCID.Lavabat:
                case NPCID.RedDevil:
                case NPCID.DemonTaxCollector:
                case NPCID.Clinger:
                case NPCID.Necromancer:
                case NPCID.Hornet:
                case NPCID.HornetFatty:
                case NPCID.HornetHoney:
                case NPCID.HornetLeafy:
                case NPCID.HornetSpikey:
                case NPCID.HornetStingy:
                case NPCID.MossHornet:
                case NPCID.BigHornetFatty:
                case NPCID.BigHornetHoney:
                case NPCID.BigHornetLeafy:
                case NPCID.BigHornetSpikey:
                case NPCID.BigHornetStingy:
                case NPCID.BigMossHornet:
                case NPCID.LittleHornetFatty:
                case NPCID.LittleHornetHoney:
                case NPCID.LittleHornetLeafy:
                case NPCID.LittleHornetSpikey:
                case NPCID.LittleHornetStingy:
                case NPCID.LittleMossHornet:
                case NPCID.Bee:
                case NPCID.CorruptSlime:
                case NPCID.Corruptor:
                case NPCID.Crimslime:
                case NPCID.CorruptBunny:
                case NPCID.CorruptGoldfish:
                case NPCID.CorruptPenguin:
                    FireNPC.Add(type);
                    npc.SetElementEffectivenessByElement(Element.Fire);
                    break;

                case NPCID.DarkCaster:
                case NPCID.ZombieEskimo:
                case NPCID.ArmedZombieEskimo:
                case NPCID.IceBat:
                case NPCID.IceSlime:
                case NPCID.SpikedIceSlime:
                case NPCID.IceElemental:
                case NPCID.IceMimic:
                case NPCID.IceTortoise:
                case NPCID.IcyMerman:
                case NPCID.AngryNimbus:
                case NPCID.IceGolem:
                case NPCID.BloodCrawler:
                case NPCID.Crimera:
                case NPCID.DoctorBones:
                case NPCID.FaceMonster:
                case NPCID.JungleBat:
                case NPCID.JungleSlime:
                case NPCID.SpikedJungleSlime:
                case NPCID.MaggotZombie:
                case NPCID.ManEater:
                case NPCID.Snatcher:
                case NPCID.ZombieMushroom:
                case NPCID.WallCreeper:
                case NPCID.WallCreeperWall:
                case NPCID.Zombie:
                case NPCID.AngryTrapper:
                case NPCID.BlackRecluse:
                case NPCID.BloodFeeder:
                case NPCID.BloodJelly:
                case NPCID.BloodMummy:
                case NPCID.DarkMummy:
                case NPCID.FloatyGross:
                case NPCID.FlyingSnake:
                case NPCID.GiantFlyingFox:
                case NPCID.Herpling:
                case NPCID.IchorSticker:
                case NPCID.JungleCreeper:
                case NPCID.DesertLamiaDark:
                case NPCID.DesertLamiaLight:
                case NPCID.LightMummy:
                case NPCID.DesertScorpionWalk:
                case NPCID.DesertScorpionWall:
                case NPCID.Slimeling:
                case NPCID.Slimer:
                case NPCID.DesertGhoul:
                case NPCID.DesertGhoulCorruption:
                case NPCID.DesertGhoulCrimson:
                case NPCID.DesertGhoulHallow:
                case NPCID.ToxicSludge:
                case NPCID.BloodEelHead:
                case NPCID.BloodEelBody:
                case NPCID.BloodEelTail:
                case NPCID.BloodSquid:
                case NPCID.BloodZombie:
                case NPCID.BloodNautilus:
                case NPCID.Drippler:
                case NPCID.GoblinShark:
                case NPCID.TheGroom:
                case NPCID.TheBride:
                case NPCID.CrimsonBunny:
                case NPCID.CrimsonGoldfish:
                case NPCID.CrimsonPenguin:
                case NPCID.EyeballFlyingFish:
                case NPCID.ZombieMerman:
                case NPCID.SandsharkCorrupt:
                case NPCID.SandsharkCrimson:
                case NPCID.CursedSkull:
                case NPCID.GiantCursedSkull:
                case NPCID.Ghost:
                    IceNPC.Add(type);
                    npc.SetElementEffectivenessByElement(Element.Ice);
                    break;

                case NPCID.BlueJellyfish:
                case NPCID.Harpy:
                case NPCID.PinkJellyfish:
                case NPCID.GreenJellyfish:
                case NPCID.WyvernHead:
                case NPCID.WyvernBody:
                case NPCID.WyvernLegs:
                case NPCID.WyvernBody2:
                case NPCID.WyvernBody3:
                case NPCID.WyvernTail:
                case NPCID.Tumbleweed:
                case NPCID.SandShark:
                case NPCID.SandElemental:
                    ElectricNPC.Add(type);
                    npc.SetElementEffectivenessByElement(Element.Electric);
                    break;

                case NPCID.DungeonSpirit:
                case NPCID.ChaosElemental:
                case NPCID.Pixie:
                case NPCID.RaggedCaster:
                case NPCID.RaggedCasterOpenCoat:
                case NPCID.AngryBones:
                case NPCID.AnomuraFungus:
                case NPCID.Antlion:
                case NPCID.WalkingAntlion:
                case NPCID.GiantWalkingAntlion:
                case NPCID.LarvaeAntlion:
                case NPCID.FlyingAntlion:
                case NPCID.GiantFlyingAntlion:
                case NPCID.CochinealBeetle:
                case NPCID.Crab:
                case NPCID.Crawdad:
                case NPCID.Crawdad2:
                case NPCID.CyanBeetle:
                case NPCID.EaterofSouls:
                case NPCID.GiantShelly:
                case NPCID.GiantShelly2:
                case NPCID.GraniteFlyer:
                case NPCID.GraniteGolem:
                case NPCID.GreekSkeleton:
                case NPCID.LacBeetle:
                case NPCID.SeaSnail:
                case NPCID.Skeleton:
                case NPCID.SpikeBall:
                case NPCID.SporeSkeleton:
                case NPCID.UndeadMiner:
                case NPCID.UndeadViking:
                case NPCID.ArmoredSkeleton:
                case NPCID.ArmoredViking:
                case NPCID.BlueArmoredBones:
                case NPCID.BlueArmoredBonesMace:
                case NPCID.BlueArmoredBonesNoPants:
                case NPCID.BlueArmoredBonesSword:
                case NPCID.BoneLee:
                case NPCID.MartianProbe:
                case NPCID.Mimic:
                case NPCID.PresentMimic:
                case NPCID.Paladin:
                case NPCID.PossessedArmor:
                case NPCID.RockGolem:
                case NPCID.RustyArmoredBonesAxe:
                case NPCID.RustyArmoredBonesFlail:
                case NPCID.RustyArmoredBonesSword:
                case NPCID.RustyArmoredBonesSwordNoArmor:
                case NPCID.SkeletonArcher:
                case NPCID.SkeletonCommando:
                case NPCID.SkeletonSniper:
                case NPCID.TacticalSkeleton:
                case NPCID.SandsharkHallow:
                case NPCID.GoblinThief:
                case NPCID.GoblinWarrior:
                case NPCID.BigMimicCorruption:
                case NPCID.BigMimicCrimson:
                case NPCID.BigMimicJungle:
                case NPCID.CrimsonAxe:
                case NPCID.CursedHammer:
                case NPCID.EnchantedSword:
                case NPCID.BigMimicHallow:
                    MetalNPC.Add(type);
                    npc.SetElementEffectivenessByElement(Element.Metal);
                    break;

                case NPCID.MeteorHead:
                case NPCID.Tim:
                case NPCID.ChaosBall:
                case NPCID.ChaosBallTim:
                case NPCID.HellArmoredBones:
                case NPCID.HellArmoredBonesMace:
                case NPCID.HellArmoredBonesSpikeShield:
                case NPCID.HellArmoredBonesSword:
                case NPCID.RuneWizard:
                case NPCID.GoblinSummoner:
                case NPCID.ShadowFlameApparition:
                    FireNPC.Add(type);
                    MetalNPC.Add(type);
                    npc.SetElementEffectivenessMultipliers(0.8, 1.5, 1.0, 0.6);
                    break;
            }
        }
    }
}
