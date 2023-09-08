using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.DedicatedItems.MrGerd26;
using ShardsOfAtheria.Items.DedicatedItems.TheEternalAce;
using ShardsOfAtheria.Items.DedicatedItems.Webmillio;
using ShardsOfAtheria.Items.Tools.Mining;
using ShardsOfAtheria.Items.Weapons;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
using System.Collections.Generic;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ShardsOfAtheria.CrossMod.ElementsMod
{
    [JITWhenModsEnabled("BattleNetworkElements")]
    internal class SetItemElements : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.ElementModEnabled;
        }

        static readonly List<int> Fire = new()
        {
            ItemType<DeckOfCards>(),
            ItemType<WormTench>(),
            ItemType<GenesisAndRagnarok>(),
            ItemType<HeroSword>(),
            ItemType<Satanlance>(),
            ItemType<TheMessiah>(),
            ItemType<Yamiko>(),
            ItemType<WormBloom>(),
            ItemType<Zenova>(),
            ItemType<AreusFlameCannon>(),
            ItemType<AreusGrenade>(),
            ItemType<DoubleBow>(),
            ItemType<HandCanon>(),
            ItemType<HeroGun>(),
            ItemType<FlailOfFlesh>(),
            ItemType<Prometheus>(),
        };
        static readonly List<int> Aqua = new()
        {
            ItemType<AncientMedalion>(),
            ItemType<DeckOfCards>(),
            ItemType<SpectralArrow>(),
            ItemType<SpectralBullet>(),
            ItemType<SpectralBullet>(),
            ItemType<PerishSong>(),
            ItemType<CrossDagger>(),
            ItemType<EntropySlicer>(),
            ItemType<GenesisAndRagnarok>(),
            ItemType<TheMourningStar>(),
            ItemType<Zenova>(),
            ItemType<BlackHoleRepeater>(),
            ItemType<KingsKusarigama>(),
            ItemType<PhantomRose>(),
            ItemType<Pandora>(),
        };
        static readonly List<int> Elec = new()
        {
            ItemType<HardlightBraces>(),
            ItemType<ValkyrieCrown>(),

            ItemType<ValkyrieCrest>(),

            ItemType<AlphaSpectrum>(),
            ItemType<DeckOfCards>(),
            ItemType<War>(),

            ItemType<RedAreusChainsaw>(),
            ItemType<RedAreusDrill>(),
            ItemType<RedAreusPickaxe>(),

            ItemType<AreusBullet>(),
            ItemType<AreusArrow>(),

            ItemType<AreusGambit>(),
            ItemType<AreusGauntlet>(),
            ItemType<AreusScepter>(),
            ItemType<AreusStaff>(),
            ItemType<AreusTrimmedTome>(),
            ItemType<Bytecrusher>(),
            ItemType<PlumeCodex>(),
            ItemType<ScreamLantern>(),

            ItemType<AreusBaton>(),
            ItemType<AreusDagger>(),
            ItemType<AreusEdge>(),
            ItemType<AreusGlaive>(),
            ItemType<AreusKatana>(),
            ItemType<AreusLance>(),
            ItemType<AreusStrikeChain>(),
            ItemType<AreusSword>(),
            ItemType<GenesisAndRagnarok>(),
            ItemType<Gomorrah>(),
            ItemType<ReactorMeltdown>(),
            ItemType<ValkyrieBlade>(),
            ItemType<ValkyrieStorm>(),
            ItemType<ValkyrieStormLance>(),
            ItemType<Yamiko>(),
            ItemType<Zenova>(),

            ItemType<AreusAssaultRifle>(),
            ItemType<AreusFlameCannon>(),
            ItemType<AreusGrenade>(),
            ItemType<AreusJavelin>(),
            ItemType<AreusMagnum>(),
            ItemType<AreusPistol>(),
            ItemType<AreusRailgun>(),
            ItemType<AreusRecursor>(),
            ItemType<AreusRocketLauncher>(),
            ItemType<Coilgun>(),
            ItemType<DownBow>(),
            ItemType<HardlightKnife>(),
            ItemType<Magnus>(),
            ItemType<Pantheon>(),
            ItemType<Scarlet>(),

            ItemType<BrokenAreusMirror>(),
            ItemType<NestlingStaff>(),
            ItemType<AreusCrate>(),

            ItemType<Pandora>(),
            ItemType<Prometheus>(),
        };
        static readonly List<int> Wood = new()
        {
            ItemType<AncientMedalion>(),
            ItemType<PottedPlant>(),
            ItemType<DeckOfCards>(),
            ItemType<WormTench>(),
            ItemType<Cataracnia>(),
            ItemType<ReactorMeltdown>(),
            ItemType<TheMourningStar>(),
            ItemType<WormBloom>(),
            ItemType<Zenova>(),
            ItemType<P90>(),
            ItemType<DragonSpineWhip>(),
            ItemType<FlailOfFlesh>(),
        };

        [JITWhenModsEnabled("BattleNetworkElements")]
        public override void SetStaticDefaults()
        {
            foreach (int i in Fire)
            {
                i.AddFireItem();
            }
            foreach (int i in Aqua)
            {
                i.AddAquaItem();
            }
            foreach (int i in Elec)
            {
                i.AddElecItem();
            }
            foreach (int i in Wood)
            {
                i.AddWoodItem();
            }
        }
    }
}
