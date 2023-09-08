using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Projectiles.NPCProj.Elizabeth;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
using ShardsOfAtheria.Projectiles.NPCProj.Variant;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Tools;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Magic.ByteCrush;
using ShardsOfAtheria.Projectiles.Weapon.Magic.ElecGauntlet;
using ShardsOfAtheria.Projectiles.Weapon.Magic.Gambit;
using ShardsOfAtheria.Projectiles.Weapon.Magic.Spectrum;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusDaggerProjs;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusGlaive;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusJustitia;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusSwordProjs;
using ShardsOfAtheria.Projectiles.Weapon.Melee.BloodDagger;
using ShardsOfAtheria.Projectiles.Weapon.Melee.BloodthirstySword;
using ShardsOfAtheria.Projectiles.Weapon.Melee.ElecKatana;
using ShardsOfAtheria.Projectiles.Weapon.Melee.EnergyScythe;
using ShardsOfAtheria.Projectiles.Weapon.Melee.EntropyCutter;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff;
using ShardsOfAtheria.Projectiles.Weapon.Melee.Gomorrah;
using ShardsOfAtheria.Projectiles.Weapon.Melee.HeroSword;
using ShardsOfAtheria.Projectiles.Weapon.Melee.OmegaSword;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.AreusUltrakillGun;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.DeckOfCards;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.EventHorizon;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.FireCannon;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.GunRose;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.VergilFlamethrower;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using ShardsOfAtheria.Projectiles.Weapon.Summon.Whip;
using System.Collections.Generic;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ShardsOfAtheria.CrossMod.ElementsMod
{
    [JITWhenModsEnabled("BattleNetworkElements")]
    internal class SetProjectileElements : GlobalProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.ElementModEnabled;
        }

        static readonly List<int> Fire = new()
        {
            ProjectileType<TheHungry>(),
            ProjectileType<TrueEyeOfCthulhuAttack>(),
            ProjectileType<Void>(),
            ProjectileType<FlamePillar>(),
            ProjectileType<AreusGrenadeHostile>(),
            ProjectileType<GolemBeam>(),
            ProjectileType<PhantasmalEye>(),
            ProjectileType<SpinPrime>(),
            ProjectileType<SpinSkull>(),
            ProjectileType<FireCannon_Fire_Gauntlet>(),
            ProjectileType<VileShot>(),
            ProjectileType<EnergyScythe>(),
            ProjectileType<RagnarokProj>(),
            ProjectileType<RagnarokProj2>(),
            ProjectileType<HeroBlade>(),
            ProjectileType<HeroSlash>(),
            ProjectileType<Messiah>(),
            ProjectileType<MessiahRanbu>(),
            ProjectileType<CorruptRose>(),
            ProjectileType<ZenovaProjectile>(),
            ProjectileType<AceOfSpades>(),
            ProjectileType<FireCannon_Fire1>(),
            ProjectileType<FireCannon_Fire2>(),
            ProjectileType<FireCannon_Fire3>(),
            ProjectileType<AreusGrenadeProj>(),
            ProjectileType<LaserArrow>(),
            ProjectileType<PrometheusFire>(),
            ProjectileType<FlailOfFleshProj>(),
        };
        static readonly List<int> Aqua = new()
        {
            ProjectileType<IceFragment>(),
            ProjectileType<Servant>(),
            ProjectileType<TrueEyeOfCthulhuAttack>(),
            ProjectileType<BloodArrowHostile>(),
            ProjectileType<BloodBubbleHostile>(),
            ProjectileType<BloodDropHostile>(),
            ProjectileType<BloodJavelinHostile>(),
            ProjectileType<BloodNeedleHostile>(),
            ProjectileType<BloodSickleHostile>(),
            ProjectileType<BloodWaveHostile>(),
            ProjectileType<Sea>(),
            ProjectileType<TidalWave>(),
            ProjectileType<CreeperHitbox>(),
            ProjectileType<PhantasmalEye>(),
            ProjectileType<SpectralArrowProj>(),
            ProjectileType<SpectralBulletProj>(),
            ProjectileType<DeathNote>(),
            ProjectileType<IceBolt>(),
            ProjectileType<HolyBloodOffense>(),
            ProjectileType<MourningStar>(),
            ProjectileType<BloodCutter>(),
            ProjectileType<EntropyBlade>(),
            ProjectileType<EntropySickle>(),
            ProjectileType<EntropySlash>(),
            ProjectileType<IceShard>(),
            ProjectileType<IceVortexShard>(),
            ProjectileType<Genesis_Spear>(),
            ProjectileType<Genesis_Spear2>(),
            ProjectileType<Genesis_Sword>(),
            ProjectileType<Genesis_Whip>(),
            ProjectileType<ZenovaProjectile>(),
            ProjectileType<AceOfHearts>(),
            ProjectileType<BlackHole>(),
            ProjectileType<BlackHoleBolt>(),
            ProjectileType<KusarigamaKing>(),
        };
        static readonly List<int> Elec = new()
        {
            ProjectileType<AreusMirrorShard>(),
            ProjectileType<YoungHarpy>(),
            ProjectileType<ElectricTrail>(),
            ProjectileType<FeatherBlade>(),
            ProjectileType<HardlightKnifeHostile>(),
            ProjectileType<LightningBolt>(),
            ProjectileType<StormCloud>(),
            ProjectileType<StormLance>(),
            ProjectileType<StormSword>(),
            ProjectileType<Static>(),
            ProjectileType<AreusGrenadeHostile>(),

            ProjectileType<ElectricTrailFriendly>(),
            ProjectileType<PhantasmalEye>(),
            ProjectileType<SpinPrime>(),

            ProjectileType<RedAreusChainsawProj>(),
            ProjectileType<RedAreusDrillProj>(),

            ProjectileType<AreusArrowProj>(),
            ProjectileType<AreusBulletProj>(),

            ProjectileType<BitBlock>(),
            ProjectileType<AreusArrowProj_Gauntlet>(),
            ProjectileType<AreusBulletProj_Gauntlet>(),
            ProjectileType<FireCannon_Fire_Gauntlet>(),
            ProjectileType<LightningBolt_Gauntlet>(),
            ProjectileType<ElecBlade>(),
            ProjectileType<ElecDagger>(),
            ProjectileType<ElecKnuckle>(),
            ProjectileType<SpectrumLaser>(),
            ProjectileType<ElecCoin>(),
            ProjectileType<ElecScorpionTail>(),
            ProjectileType<AreusShardProj>(),
            ProjectileType<ElectricOrb>(),
            ProjectileType<ElectricSpike>(),
            ProjectileType<HardlightFeatherMagic>(),
            ProjectileType<ScreamShockwave>(),

            ProjectileType<AreusDaggerProj>(),
            ProjectileType<AreusDaggerCurrent>(),
            ProjectileType<AreusGlaive_Swing>(),
            ProjectileType<AreusGlaive_Throw>(),
            ProjectileType<AreusGlaive_Thrust>(),
            ProjectileType<AreusGlaive_Thrust2>(),
            ProjectileType<AreusJustitia>(),
            ProjectileType<AreusJustitia_Slash>(),
            ProjectileType<AreusJustitia_Stab>(),
            ProjectileType<AreusSwordProj>(),
            ProjectileType<ElectricBlade>(),
            ProjectileType<ElecKatana>(),
            ProjectileType<ElecKunai>(),
            ProjectileType<ElecKunaiHoming>(),
            ProjectileType<ElecShuriken>(),
            ProjectileType<EnergyScythe>(),
            ProjectileType<EnergyWave>(),
            ProjectileType<Genesis_Spear>(),
            ProjectileType<Genesis_Spear2>(),
            ProjectileType<Genesis_Sword>(),
            ProjectileType<Genesis_Whip>(),
            ProjectileType<Gomorrah_Burst>(),
            ProjectileType<Gomorrah_Javelin>(),
            ProjectileType<Gomorrah_Spear>(),
            ProjectileType<AreusLanceProj>(),
            ProjectileType<ElecBaton>(),
            ProjectileType<FeatherBladeFriendly>(),
            ProjectileType<HardlightBlade>(),
            ProjectileType<HardlightSlash>(),
            ProjectileType<HardlightSword>(),
            ProjectileType<PrototypeBandBlade>(),
            ProjectileType<ReactorMeltdownProj>(),
            ProjectileType<StrikeChain>(),
            ProjectileType<StrikeChainCurrent>(),
            ProjectileType<ValkyrieStormLanceProj>(),
            ProjectileType<ValkyrieStormSword>(),
            ProjectileType<Warframe>(),
            ProjectileType<Warframe_Upgrade>(),
            ProjectileType<WarframeSlash>(),
            ProjectileType<ZenovaProjectile>(),

            ProjectileType<AreusBounceShot>(),
            ProjectileType<AreusLaser>(),
            ProjectileType<AceOfDiamonds>(),
            ProjectileType<FireCannon_Fire1>(),
            ProjectileType<FireCannon_Fire2>(),
            ProjectileType<FireCannon_Fire3>(),
            ProjectileType<AreusShardProjRanged>(),
            ProjectileType<AmbassadorShot>(),
            ProjectileType<AreusGrenadeProj>(),
            ProjectileType<AreusJavelinThrown>(),
            ProjectileType<HardlightKnifeProj>(),
            ProjectileType<ScarletSpark>(),
        };
        static readonly List<int> Wood = new()
        {
            ProjectileType<Servant>(),
            ProjectileType<TheHungry>(),
            ProjectileType<TrueEyeOfCthulhuAttack>(),
            ProjectileType<YourTentacle>(),
            ProjectileType<BloodArrowHostile>(),
            ProjectileType<BloodBubbleHostile>(),
            ProjectileType<BloodDropHostile>(),
            ProjectileType<BloodJavelinHostile>(),
            ProjectileType<BloodNeedleHostile>(),
            ProjectileType<BloodSickleHostile>(),
            ProjectileType<BloodWaveHostile>(),
            ProjectileType<Poison>(),
            ProjectileType<CactusNeedle>(),
            ProjectileType<CreeperHitbox>(),
            ProjectileType<PhantasmalEye>(),
            ProjectileType<Stinger>(),
            ProjectileType<VenomSeed>(),
            ProjectileType<VileShot>(),
            ProjectileType<HolyBloodOffense>(),
            ProjectileType<MourningStar>(),
            ProjectileType<BloodCutter>(),
            ProjectileType<CorruptRose>(),
            ProjectileType<CorruptPetal>(),
            ProjectileType<ReactorMeltdownProj>(),
            ProjectileType<ZenovaProjectile>(),
            ProjectileType<AceOfClubs>(),
            ProjectileType<WitheringPetal>(),
            ProjectileType<WitheringSeed>(),
            ProjectileType<BallinBall>(),
            ProjectileType<DragonBone>(),
            ProjectileType<DragonSpineWhipProj>(),
            ProjectileType<FlailOfFleshProj>(),
        };

        [JITWhenModsEnabled("BattleNetworkElements")]
        public override void SetStaticDefaults()
        {
            foreach (int i in Fire)
            {
                i.AddFireProjectile();
            }
            foreach (int i in Aqua)
            {
                i.AddAquaProjectile();
            }
            foreach (int i in Elec)
            {
                i.AddElecProjectile();
            }
            foreach (int i in Wood)
            {
                i.AddWoodProjectile();
            }
        }
    }
}
