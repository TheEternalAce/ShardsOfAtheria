using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals.Elements
{
    public class AssignWeaponElements : GlobalItem
    {
        List<int> FireWeapon = SoAGlobalItem.FireWeapon;
        List<int> IceWeapon = SoAGlobalItem.IceWeapon;
        List<int> ElectricWeapon = SoAGlobalItem.ElectricWeapon;
        List<int> MetalWeapon = SoAGlobalItem.MetalWeapon;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            int type = item.type;
            switch (type)
            {
                case ItemID.CopperBroadsword:
                case ItemID.CopperShortsword:
                case ItemID.CopperPickaxe:
                case ItemID.CopperAxe:
                case ItemID.CopperHammer:

                case ItemID.TinBroadsword:
                case ItemID.TinShortsword:
                case ItemID.TinPickaxe:
                case ItemID.TinAxe:
                case ItemID.TinHammer:

                case ItemID.IronBroadsword:
                case ItemID.IronShortsword:
                case ItemID.IronPickaxe:
                case ItemID.IronAxe:
                case ItemID.IronHammer:

                case ItemID.LeadBroadsword:
                case ItemID.LeadShortsword:
                case ItemID.LeadPickaxe:
                case ItemID.LeadAxe:
                case ItemID.LeadHammer:

                case ItemID.SilverBroadsword:
                case ItemID.SilverShortsword:
                case ItemID.SilverPickaxe:
                case ItemID.SilverAxe:
                case ItemID.SilverHammer:

                case ItemID.TungstenBroadsword:
                case ItemID.TungstenShortsword:
                case ItemID.TungstenPickaxe:
                case ItemID.TungstenAxe:
                case ItemID.TungstenHammer:

                case ItemID.GoldBroadsword:
                case ItemID.GoldShortsword:
                case ItemID.GoldPickaxe:
                case ItemID.GoldAxe:
                case ItemID.GoldHammer:

                case ItemID.CnadyCanePickaxe:
                case ItemID.CandyCaneSword:

                case ItemID.FossilPickaxe:

                case ItemID.BonePickaxe:

                case ItemID.PlatinumBroadsword:
                case ItemID.PlatinumShortsword:
                case ItemID.PlatinumPickaxe:
                case ItemID.PlatinumAxe:
                case ItemID.PlatinumHammer:

                case ItemID.BloodButcherer:
                case ItemID.TheRottedFork:
                case ItemID.DeathbringerPickaxe:
                case ItemID.BloodLustCluster:
                case ItemID.FleshGrinder:

                case ItemID.LightsBane:
                case ItemID.NightmarePickaxe:
                case ItemID.WarAxeoftheNight:
                case ItemID.TheBreaker:

                case ItemID.CobaltSword:
                case ItemID.CobaltNaginata:
                case ItemID.CobaltPickaxe:
                case ItemID.CobaltWaraxe:

                case ItemID.PalladiumSword:
                case ItemID.PalladiumPike:
                case ItemID.PalladiumPickaxe:
                case ItemID.PalladiumWaraxe:

                case ItemID.MythrilSword:
                case ItemID.MythrilHalberd:
                case ItemID.MythrilPickaxe:
                case ItemID.MythrilWaraxe:

                case ItemID.OrichalcumSword:
                case ItemID.OrichalcumHalberd:
                case ItemID.OrichalcumPickaxe:
                case ItemID.OrichalcumWaraxe:

                case ItemID.AdamantiteSword:
                case ItemID.AdamantiteGlaive:
                case ItemID.AdamantitePickaxe:
                case ItemID.AdamantiteWaraxe:

                case ItemID.TitaniumSword:
                case ItemID.TitaniumTrident:
                case ItemID.TitaniumPickaxe:
                case ItemID.TitaniumWaraxe:

                case ItemID.PickaxeAxe:
                case ItemID.Drax:

                case ItemID.SpectrePickaxe:
                case ItemID.SpectreHamaxe:

                case ItemID.ChlorophyteSaber:
                case ItemID.ChlorophyteClaymore:
                case ItemID.ChlorophytePartisan:
                case ItemID.ChlorophytePickaxe:
                case ItemID.ChlorophyteGreataxe:
                case ItemID.ChlorophyteWarhammer:
                case ItemID.ChlorophyteJackhammer:

                case ItemID.ShroomiteDiggingClaw:

                case ItemID.Picksaw:

                case ItemID.LucyTheAxe:
                case ItemID.TheAxe:

                case ItemID.Rockfish:
                case ItemID.Pwnhammer:
                case 4317: // Haemorrhaxe
                case ItemID.Hammush:
                    MetalWeapon.Add(type);
                    break;

                case ItemID.ReaverShark:
                case ItemID.SawtoothShark:
                case ItemID.CactusPickaxe:
                case ItemID.CactusSword:
                    IceWeapon.Add(type);
                    break;

                case ItemID.MoltenPickaxe:
                case ItemID.SolarFlarePickaxe:
                case ItemID.MeteorHamaxe:
                case ItemID.MoltenHamaxe:
                case ItemID.LunarHamaxeSolar:
                case ItemID.SolarFlareAxe:
                case ItemID.SolarFlareHammer:
                    FireWeapon.Add(type);
                    MetalWeapon.Add(type);
                    break;

                case ItemID.VortexPickaxe:
                case ItemID.LunarHamaxeVortex:
                case ItemID.VortexAxe:
                case ItemID.VortexHammer:
                    ElectricWeapon.Add(type);
                    MetalWeapon.Add(type);
                    break;

                case ItemID.NebulaPickaxe:
                case ItemID.LunarHamaxeNebula:
                case ItemID.NebulaAxe:
                case ItemID.NebulaHammer:
                case ItemID.StardustPickaxe:
                case ItemID.LunarHamaxeStardust:
                case ItemID.StardustAxe:
                case ItemID.StardustHammer:
                    IceWeapon.Add(type);
                    MetalWeapon.Add(type);
                    break;
            }
        }
    }
}
