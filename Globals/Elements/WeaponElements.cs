using Microsoft.Xna.Framework;
using ShardsOfAtheria.Config;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals.Elements
{
    public class WeaponElements : GlobalItem
    {
        public static List<int> MetalWeapon = new();
        public static List<int> FireWeapon = new();
        public static List<int> IceWeapon = new();
        public static List<int> ElectricWeapon = new();

        public override bool InstancePerEntity => true;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int type = item.type;
            if (ModContent.GetInstance<ShardsServerSideConfig>().experimental)
            {
                if (FireWeapon.Contains(type))
                {
                    tooltips.Add(new TooltipLine(Mod, "Element", Language.GetTextValue("Mods.ShardsOfAtheria.Common.ElementFire"))
                    {
                        OverrideColor = Color.Firebrick
                    });
                }
                if (IceWeapon.Contains(type))
                {
                    tooltips.Add(new TooltipLine(Mod, "Element", Language.GetTextValue("Mods.ShardsOfAtheria.Common.ElementIce"))
                    {
                        OverrideColor = Color.LightSkyBlue
                    });
                }
                if (ElectricWeapon.Contains(type))
                {
                    tooltips.Add(new TooltipLine(Mod, "Element", Language.GetTextValue("Mods.ShardsOfAtheria.Common.ElementElectric"))
                    {
                        OverrideColor = Color.Cyan
                    });
                }
                if (MetalWeapon.Contains(type))
                {
                    tooltips.Add(new TooltipLine(Mod, "Element", Language.GetTextValue("Mods.ShardsOfAtheria.Common.ElementMetal"))
                    {
                        OverrideColor = Color.Gray
                    });
                }
            }
        }

        public override void SetDefaults(Item item)
        {
            int type = item.type;
            if (ModContent.GetInstance<ShardsServerSideConfig>().experimental)
            {
                switch (type)
                {
                    case ItemID.CopperBroadsword:
                    case ItemID.CopperShortsword:
                    case ItemID.CopperBow:
                    case ItemID.AmethystStaff:
                    case ItemID.CopperPickaxe:
                    case ItemID.CopperAxe:
                    case ItemID.CopperHammer:

                    case ItemID.TinBroadsword:
                    case ItemID.TinShortsword:
                    case ItemID.TinBow:
                    case ItemID.TopazStaff:
                    case ItemID.TinPickaxe:
                    case ItemID.TinAxe:
                    case ItemID.TinHammer:

                    case ItemID.Umbrella:
                    case ItemID.Spear:
                    case ItemID.Mace:
                    case ItemID.Shuriken:
                    case ItemID.ThrowingKnife:
                    case ItemID.Grenade:
                    case ItemID.StickyGrenade:
                    case ItemID.BouncyGrenade:

                    case ItemID.IronBroadsword:
                    case ItemID.IronShortsword:
                    case ItemID.IronBow:
                    case ItemID.IronPickaxe:
                    case ItemID.IronAxe:
                    case ItemID.IronHammer:

                    case ItemID.LeadBroadsword:
                    case ItemID.LeadShortsword:
                    case ItemID.LeadBow:
                    case ItemID.LeadPickaxe:
                    case ItemID.LeadAxe:
                    case ItemID.LeadHammer:

                    case ItemID.BladedGlove:
                    case ItemID.BloodyMachete:
                    case ItemID.ChainKnife:
                    case ItemID.Javelin:

                    case ItemID.SilverBroadsword:
                    case ItemID.SilverShortsword:
                    case ItemID.SilverBow:
                    case ItemID.SapphireStaff:
                    case ItemID.SilverPickaxe:
                    case ItemID.SilverAxe:
                    case ItemID.SilverHammer:

                    case ItemID.TungstenBroadsword:
                    case ItemID.TungstenShortsword:
                    case ItemID.TungstenBow:
                    case ItemID.EmeraldStaff:
                    case ItemID.TungstenPickaxe:
                    case ItemID.TungstenAxe:
                    case ItemID.TungstenHammer:

                    case ItemID.GoldBroadsword:
                    case ItemID.GoldShortsword:
                    case ItemID.GoldBow:
                    case ItemID.RubyStaff:
                    case ItemID.GoldPickaxe:
                    case ItemID.GoldAxe:
                    case ItemID.GoldHammer:

                    case ItemID.StylistKilLaKillScissorsIWish:
                    case ItemID.EnchantedBoomerang:
                    case ItemID.BoneDagger:
                    case ItemID.BoneJavelin:
                    case ItemID.PartyGirlGrenade:

                    case ItemID.PlatinumBroadsword:
                    case ItemID.PlatinumShortsword:
                    case ItemID.PlatinumBow:
                    case ItemID.DiamondStaff:
                    case ItemID.PlatinumPickaxe:
                    case ItemID.PlatinumAxe:
                    case ItemID.PlatinumHammer:

                    // Post BoC/EoW
                    case ItemID.Gladius:
                    case ItemID.BoneSword:
                    case ItemID.Katana:
                    case ItemID.DyeTradersScimitar:
                    case ItemID.Starfury:
                    case ItemID.EnchantedSword:
                    case ItemID.Terragrim:
                    case ItemID.FalconBlade:
                    case ItemID.Rally:
                    case ItemID.SpikyBall:
                    case ItemID.Harpoon:
                    case ItemID.DD2BallistraTowerT1Popper:

                    case ItemID.BloodButcherer:
                    case ItemID.TheMeatball:
                    case ItemID.TendonBow:
                    case ItemID.CrimsonYoyo:
                    case ItemID.TheRottedFork:
                    case ItemID.DeathbringerPickaxe:
                    case ItemID.BloodLustCluster:
                    case ItemID.FleshGrinder:

                    case ItemID.LightsBane:
                    case ItemID.DemonBow:
                    case ItemID.BallOHurt:
                    case ItemID.CorruptYoyo:
                    case ItemID.NightmarePickaxe:
                    case ItemID.WarAxeoftheNight:
                    case ItemID.TheBreaker:

                    // Post Skeletron
                    case ItemID.TragicUmbrella:
                    case ItemID.DarkLance:
                    case ItemID.Code1:
                    case ItemID.Valor:
                    case ItemID.CombatWrench:
                    case ItemID.BlueMoon:
                    case ItemID.Muramasa:
                    case ItemID.NightsEdge:

                    // Hardmode
                    case ItemID.BreakerBlade:
                    case ItemID.TaxCollectorsStickOfDoom:
                    case ItemID.FormatC:
                    case ItemID.Gradient:
                    case ItemID.Chik:
                    case ItemID.JoustingLance:
                    case ItemID.FlyingKnife:
                    case ItemID.ChainGuillotines:
                    case ItemID.BouncingShield:
                    case ItemID.DaoofPow:
                    case ItemID.Anchor:
                    case ItemID.CrystalSerpent:
                    case ItemID.CrystalVileShard:
                    case ItemID.CrystalStorm:

                    case ItemID.CobaltSword:
                    case ItemID.CobaltNaginata:
                    case ItemID.CobaltPickaxe:
                    case ItemID.CobaltWaraxe:

                    case ItemID.PalladiumSword:
                    case ItemID.PalladiumPike:
                    case ItemID.PalladiumPickaxe:
                    case ItemID.PalladiumWaraxe:

                    case ItemID.Code2:
                    case ItemID.Cutlass:
                    case ItemID.BeamSword:
                    case ItemID.Smolstar:
                    case ItemID.PirateStaff:
                    case ItemID.PygmyStaff:

                    case ItemID.MythrilSword:
                    case ItemID.MythrilHalberd:
                    case ItemID.MythrilPickaxe:
                    case ItemID.MythrilWaraxe:

                    case ItemID.OrichalcumSword:
                    case ItemID.OrichalcumHalberd:
                    case ItemID.OrichalcumPickaxe:
                    case ItemID.OrichalcumWaraxe:

                    case ItemID.Arkhalis:
                    case ItemID.FetidBaghnakhs:
                    case ItemID.Yelets:
                    case ItemID.RedsYoyo:

                    case ItemID.AdamantiteSword:
                    case ItemID.AdamantiteGlaive:
                    case ItemID.AdamantitePickaxe:
                    case ItemID.AdamantiteWaraxe:

                    case ItemID.TitaniumSword:
                    case ItemID.TitaniumTrident:
                    case ItemID.TitaniumPickaxe:
                    case ItemID.TitaniumWaraxe:

                    // Post Mechs
                    case ItemID.ObsidianSwordfish:
                    case ItemID.Excalibur:
                    case ItemID.Gungnir:
                    case ItemID.LightDisc:
                    case ItemID.HallowJoustingLance:
                    case ItemID.DD2SquireDemonSword:
                    case ItemID.MonkStaffT1:
                    case ItemID.RainbowRod:
                    case ItemID.DD2BallistraTowerT2Popper:

                    case ItemID.PickaxeAxe:
                    case ItemID.Drax:

                    // Post Plantera
                    case ItemID.SpectrePickaxe:
                    case ItemID.SpectreHamaxe:

                    case ItemID.ChlorophyteSaber:
                    case ItemID.ChlorophyteClaymore:
                    case ItemID.ChlorophytePartisan:
                    case ItemID.ChlorophytePickaxe:
                    case ItemID.ChlorophyteGreataxe:
                    case ItemID.ChlorophyteWarhammer:
                    case ItemID.ChlorophyteJackhammer:

                    case ItemID.TrueExcalibur:
                    case ItemID.TrueNightsEdge:
                    case ItemID.Seedler:
                    case ItemID.DeathSickle:
                    case ItemID.Keybrand:
                    case ItemID.Kraken:
                    case ItemID.ShadowJoustingLance:
                    case ItemID.PaladinsHammer:
                    case ItemID.PsychoKnife:
                    case ItemID.TheEyeOfCthulhu:
                    case ItemID.TerraBlade:
                    case ItemID.PiranhaGun:

                    // Post Golem
                    case ItemID.PossessedHatchet:
                    case ItemID.GolemFist:
                    case ItemID.PiercingStarlight:
                    case ItemID.Terrarian:
                    case ItemID.StarWrath:
                    case ItemID.Meowmere:
                    case ItemID.Zenith:
                    case ItemID.RainbowCrystalStaff:
                    case ItemID.DD2BallistraTowerT3Popper:

                    // Tool
                    case ItemID.ShroomiteDiggingClaw:
                    case ItemID.Picksaw:
                    case ItemID.CnadyCanePickaxe:
                    case ItemID.FossilPickaxe:
                    case ItemID.BonePickaxe:
                    case ItemID.LucyTheAxe:
                    case ItemID.TheAxe:
                    case ItemID.Rockfish:
                    case ItemID.Pwnhammer:
                    //case 4317: // Haemorrhaxe
                    case ItemID.Hammush:
                        MetalWeapon.Add(type);
                        break;

                    case ItemID.FlamingMace:
                    case ItemID.WandofSparking:
                    case ItemID.FlareGun:
                    case ItemID.PoisonedKnife:

                    // Post BoC/EoW
                    case ItemID.BluePhaseblade:
                    case ItemID.GreenPhaseblade:
                    case ItemID.OrangePhaseblade:
                    case ItemID.PurplePhaseblade:
                    case ItemID.RedPhaseblade:
                    case ItemID.WhitePhaseblade:
                    case ItemID.YellowPhaseblade:
                    case ItemID.MolotovCocktail:
                    case ItemID.VampireFrogStaff:
                    case ItemID.DD2ExplosiveTrapT1Popper:
                    case ItemID.DD2FlameburstTowerT1Popper:

                    // Post Skeletron
                    case ItemID.MagicMissile:
                    case ItemID.BookofSkulls:
                    case ItemID.DemonScythe:
                    case ItemID.Cascade:
                    case ItemID.Flamarang:
                    case ItemID.Sunfury:
                    case ItemID.Flamelash:
                    case ItemID.FlowerofFire:
                    case ItemID.MoltenFury:
                    case ItemID.HellwingBow:
                    case ItemID.PhoenixBlaster:
                    case ItemID.BoneGlove:
                    case ItemID.AleThrowingGlove:
                    case ItemID.HoundiusShootius:
                    case ItemID.HornetStaff:
                    case ItemID.ImpStaff:

                    // Hardmode
                    case ItemID.HelFire:
                    case ItemID.ShadowFlameKnife:
                    case ItemID.BluePhasesaber:
                    case ItemID.GreenPhasesaber:
                    case ItemID.OrangePhasesaber:
                    case ItemID.PurplePhasesaber:
                    case ItemID.RedPhasesaber:
                    case ItemID.WhitePhasesaber:
                    case ItemID.YellowPhasesaber:
                    case ItemID.MeteorStaff:
                    case ItemID.UnholyTrident:
                    case ItemID.CursedFlame:
                    case ItemID.ClingerStaff:
                    case ItemID.SpiritFlame:
                    case ItemID.ShadowFlameHexDoll:
                    case ItemID.ShadowFlameBow:
                    case ItemID.SpiderStaff:
                    case ItemID.QueenSpiderStaff:

                    // Post Mechs
                    case ItemID.MonkStaffT2:
                    case ItemID.ShadowbeamStaff:
                    case ItemID.InfernoFork:
                    case ItemID.DD2PhoenixBow:
                    case ItemID.Flamethrower:
                    case ItemID.DD2ExplosiveTrapT2Popper:
                    case ItemID.DD2FlameburstTowerT2Popper:
                    case ItemID.OpticStaff:

                    // Post Plantera
                    case ItemID.JackOLanternLauncher:

                    case ItemID.HeatRay:
                    case ItemID.DD2SquireBetsySword:
                    case ItemID.MonkStaffT3:
                    case ItemID.SolarEruption:
                    case ItemID.DayBreak:
                    case ItemID.ApprenticeStaffT3:
                    case ItemID.LunarFlareBook:
                    case ItemID.FairyQueenMagicItem:
                    case ItemID.NebulaBlaze:
                    case ItemID.LastPrism:
                    case ItemID.DD2BetsyBow:
                    case ItemID.EldMelter:
                    case ItemID.DD2ExplosiveTrapT3Popper:
                    case ItemID.DD2FlameburstTowerT3Popper:

                    // Tool
                    case ItemID.MoltenPickaxe:
                    case ItemID.SolarFlarePickaxe:
                    case ItemID.MeteorHamaxe:
                    case ItemID.MoltenHamaxe:
                    case ItemID.LunarHamaxeSolar:
                    case ItemID.SolarFlareAxe:
                    case ItemID.SolarFlareHammer:
                        FireWeapon.Add(type);
                        break;

                    //case ItemID.WandofFrosting:
                    case ItemID.CandyCaneSword:
                    case ItemID.IceBlade:
                    case ItemID.IceBoomerang:
                    case ItemID.SnowballCannon:
                    case ItemID.Snowball:
                    case ItemID.FlinxStaff:
                    case ItemID.AbigailsFlower:
                    case ItemID.SlimeStaff:

                    // Post BoC/EoW
                    case ItemID.CrimsonRod:
                    case ItemID.BloodRainBow:

                    // Post Skeletron
                    case ItemID.AquaScepter:
                    case ItemID.WaterBolt:

                    // Hardmode
                    case ItemID.Frostbrand:
                    case ItemID.Amarok:
                    case ItemID.SoulDrain:
                    case ItemID.PoisonStaff:
                    case ItemID.FrostStaff:
                    case ItemID.FlowerofFrost:
                    case ItemID.GoldenShower:
                    case ItemID.IceRod:
                    case ItemID.SharpTears:
                    case ItemID.NimbusRod:
                    case ItemID.IceBow:
                    case ItemID.SanguineStaff:

                    // Post Plantera
                    case ItemID.NorthPole:
                    case ItemID.VenomStaff:
                    case ItemID.SpectreStaff:
                    case ItemID.BlizzardStaff:
                    case ItemID.ToxicFlask:
                    case ItemID.Toxikarp:

                    // Post Golem
                    case ItemID.Flairon:
                    case ItemID.BubbleGun:
                    case ItemID.RazorbladeTyphoon:
                    case ItemID.NebulaArcanum:
                    case ItemID.Tsunami:
                    case ItemID.TempestStaff:
                    case ItemID.StardustCellStaff:
                    case ItemID.StardustDragonStaff:
                    case ItemID.StaffoftheFrostHydra:

                    // Tool
                    case ItemID.Swordfish:
                    case ItemID.ReaverShark:
                    case ItemID.SawtoothShark:
                    case ItemID.NebulaPickaxe:
                    case ItemID.LunarHamaxeNebula:
                    case ItemID.NebulaAxe:
                    case ItemID.NebulaHammer:
                    case ItemID.StardustPickaxe:
                    case ItemID.LunarHamaxeStardust:
                    case ItemID.StardustAxe:
                    case ItemID.StardustHammer:
                        IceWeapon.Add(type);
                        break;

                    case ItemID.ThunderSpear:
                    case ItemID.ThunderStaff:
                    case ItemID.BabyBirdStaff:

                    // Post Skeletron
                    case ItemID.WeatherPain:
                    case ItemID.ZapinatorGray:
                    case ItemID.SpaceGun:
                    case ItemID.DD2LightningAuraT1Popper:

                    // Hardmode
                    case ItemID.LaserRifle:
                    case ItemID.SkyFracture:
                    case ItemID.MagicalHarp:
                    case ItemID.DaedalusStormbow:
                    case ItemID.ZapinatorOrange:

                    case ItemID.ValkyrieYoyo:
                    case ItemID.BookStaff:
                    case ItemID.MagnetSphere:
                    case ItemID.DD2LightningAuraT2Popper:

                    // Post Plantera
                    case ItemID.PulseBow:
                    case ItemID.RavenStaff:
                    case ItemID.StormTigerStaff:
                    case ItemID.DeadlySphereStaff:

                    // Post Golem
                    case ItemID.InfluxWaver:
                    case ItemID.LaserMachinegun:
                    case ItemID.ChargedBlasterCannon:
                    case ItemID.SparkleGuitar:
                    case ItemID.Phantasm:
                    case ItemID.VortexBeater:
                    case ItemID.ElectrosphereLauncher:
                    case ItemID.DD2LightningAuraT3Popper:
                    case ItemID.EmpressBlade:
                    case ItemID.XenoStaff:
                    case ItemID.MoonlordTurretStaff:

                    // Tool
                    case ItemID.VortexPickaxe:
                    case ItemID.LunarHamaxeVortex:
                    case ItemID.VortexAxe:
                    case ItemID.VortexHammer:
                        ElectricWeapon.Add(type);
                        break;
                }
                if (SoAGlobalItem.AreusWeapon.Contains(type))
                {
                    ElectricWeapon.Add(type);
                }
                if (item.shoot != ProjectileID.None)
                {
                    if (FireWeapon.Contains(type))
                    {
                        ProjectileElements.FireProj.Add(item.shoot);
                    }
                    if (IceWeapon.Contains(type))
                    {
                        ProjectileElements.IceProj.Add(item.shoot);
                    }
                    if (ElectricWeapon.Contains(type))
                    {
                        ProjectileElements.ElectricProj.Add(item.shoot);
                    }
                    if (MetalWeapon.Contains(type))
                    {
                        ProjectileElements.MetalProj.Add(item.shoot);
                    }
                }
            }
        }
    }
}
