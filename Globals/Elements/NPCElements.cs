using ShardsOfAtheria.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals.Elements
{
    public class NPCElements : GlobalNPC
    {
        public static List<int> MetalNPC = new();
        public static List<int> FireNPC = new();
        public static List<int> IceNPC = new();
        public static List<int> ElectricNPC = new();
        /// <summary>
        /// Elemental multipliers for a given NPC in the following order: Fire, Ice, Electric, Metal
        /// </summary>
        public double[] elementMultiplier = { 1.0, 1.0, 1.0, 1.0 };

        public override bool InstancePerEntity => true;

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            double modifier = 1.0;
            if (WeaponElements.Fire.Contains(item.type))
            {
                modifier *= elementMultiplier[Element.Fire];
            }
            if (WeaponElements.Ice.Contains(item.type))
            {
                modifier *= elementMultiplier[Element.Ice];
            }
            if (WeaponElements.Electric.Contains(item.type))
            {
                modifier *= elementMultiplier[Element.Ice];
            }
            if (WeaponElements.Metal.Contains(item.type))
            {
                modifier *= elementMultiplier[Element.Ice];
            }
            damage = (int)Math.Ceiling(damage * modifier);

            base.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ProjectileElements projectile1 = projectile.GetGlobalProjectile<ProjectileElements>();
            double modifier = 1.0;
            if (ProjectileElements.Fire.Contains(projectile.type) || projectile1.tempFire)
            {
                modifier *= elementMultiplier[Element.Fire];
            }
            if (ProjectileElements.Ice.Contains(projectile.type) || projectile1.tempIce)
            {
                modifier *= elementMultiplier[Element.Ice];
            }
            if (ProjectileElements.Electric.Contains(projectile.type) || projectile1.tempElectric)
            {
                modifier *= elementMultiplier[Element.Electric];
            }
            if (ProjectileElements.Metal.Contains(projectile.type) || projectile1.tempMetal)
            {
                modifier *= elementMultiplier[Element.Metal];
            }
            damage = (int)Math.Ceiling(damage * modifier);

            base.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }

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
                case NPCID.NecromancerArmored:

                case NPCID.WallofFlesh:
                case NPCID.WallofFleshEye:
                case NPCID.PrimeCannon:
                case NPCID.SkeletronPrime:
                case NPCID.Spazmatism:
                case NPCID.Golem:
                case NPCID.GolemFistLeft:
                case NPCID.GolemFistRight:
                case NPCID.GolemHead:
                case NPCID.GolemHeadFree:
                    FireNPC.Add(type);
                    npc.SetElementMultipliersByElement(Element.Fire);
                    break;

                case NPCID.DarkCaster:
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
                case NPCID.JungleBat:
                case NPCID.JungleSlime:
                case NPCID.SpikedJungleSlime:
                case NPCID.ManEater:
                case NPCID.Snatcher:
                case NPCID.WallCreeper:
                case NPCID.WallCreeperWall:
                case NPCID.AngryTrapper:
                case NPCID.BlackRecluse:
                case NPCID.BloodFeeder:
                case NPCID.BloodJelly:
                case NPCID.FloatyGross:
                case NPCID.FlyingSnake:
                case NPCID.GiantFlyingFox:
                case NPCID.IchorSticker:
                case NPCID.JungleCreeper:
                case NPCID.Slimeling:
                case NPCID.Slimer:
                case NPCID.ToxicSludge:
                case NPCID.BloodEelHead:
                case NPCID.BloodEelBody:
                case NPCID.BloodEelTail:
                case NPCID.BloodSquid:
                case NPCID.BloodZombie:
                case NPCID.BloodNautilus:
                case NPCID.Drippler:
                case NPCID.GoblinShark:
                case NPCID.EyeballFlyingFish:
                case NPCID.ZombieMerman:
                case NPCID.SandsharkCorrupt:
                case NPCID.SandsharkCrimson:
                case NPCID.CursedSkull:
                case NPCID.GiantCursedSkull:
                case NPCID.Ghost:
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

                case NPCID.KingSlime:
                case NPCID.EaterofWorldsHead:
                case NPCID.EaterofWorldsBody:
                case NPCID.EaterofWorldsTail:
                case NPCID.BrainofCthulhu:
                case NPCID.Creeper:
                case NPCID.QueenBee:
                case NPCID.Deerclops:
                case NPCID.DeerclopsLeg:
                case NPCID.QueenSlimeBoss:
                case NPCID.QueenSlimeMinionBlue:
                case NPCID.QueenSlimeMinionPink:
                case NPCID.QueenSlimeMinionPurple:
                case NPCID.PrimeVice:
                case NPCID.Plantera:
                case NPCID.PlanterasHook:
                case NPCID.PlanterasTentacle:
                case NPCID.DukeFishron:
                case NPCID.Sharkron:
                case NPCID.Sharkron2:
                    IceNPC.Add(type);
                    npc.SetElementMultipliersByElement(Element.Ice);
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
                case NPCID.DesertLamiaDark:
                case NPCID.DesertLamiaLight:
                case NPCID.DesertScorpionWalk:
                case NPCID.DesertScorpionWall:
                case NPCID.DesertGhoul:
                case NPCID.DesertGhoulCorruption:
                case NPCID.DesertGhoulCrimson:
                case NPCID.DesertGhoulHallow:

                case NPCID.EyeofCthulhu:
                case NPCID.SkeletronHead:
                case NPCID.SkeletronHand:
                case NPCID.PrimeLaser:
                case NPCID.HallowBoss:
                    ElectricNPC.Add(type);
                    npc.SetElementMultipliersByElement(Element.Electric);
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

                case NPCID.PrimeSaw:
                case NPCID.TheDestroyer:
                case NPCID.TheDestroyerBody:
                case NPCID.TheDestroyerTail:
                    MetalNPC.Add(type);
                    npc.SetElementMultipliersByElement(Element.Metal);
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
                    npc.SetCustomElementMultipliers(0.8, 1.5, 1.0, 0.6);
                    break;

                case NPCID.CultistBoss:
                case NPCID.CultistArcherBlue:
                case NPCID.CultistArcherWhite:
                case NPCID.CultistDevote:
                case NPCID.CultistBossClone:
                case NPCID.CultistDragonHead:
                case NPCID.CultistDragonBody1:
                case NPCID.CultistDragonBody2:
                case NPCID.CultistDragonBody3:
                case NPCID.CultistDragonTail:
                case NPCID.CultistTablet:
                case NPCID.AncientCultistSquidhead:
                case NPCID.AncientDoom:
                case NPCID.AncientLight:
                case NPCID.MoonLordCore:
                case NPCID.MoonLordFreeEye:
                case NPCID.MoonLordHand:
                case NPCID.MoonLordHead:
                case NPCID.MoonLordLeechBlob:
                    FireNPC.Add(type);
                    IceNPC.Add(type);
                    ElectricNPC.Add(type);
                    MetalNPC.Add(type);
                    npc.SetCustomElementMultipliers(1f, 1f, 1f, 1f);
                    break;
            }
        }
    }
}
