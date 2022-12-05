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
        List<int> PoisonWeapon = SoAGlobalItem.MetalWeapon;
        List<int> PlasmaWeapon = SoAGlobalItem.MetalWeapon;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            int type = item.type;
            switch (type)
            {
                case ItemID.CopperPickaxe:
                case ItemID.TinPickaxe:
                case ItemID.IronPickaxe:
                case ItemID.LeadPickaxe:
                case ItemID.SilverPickaxe:
                case ItemID.TungstenPickaxe:
                case ItemID.GoldPickaxe:
                case ItemID.CnadyCanePickaxe:
                case ItemID.FossilPickaxe:
                case ItemID.BonePickaxe:
                case ItemID.PlatinumPickaxe:
                case ItemID.NightmarePickaxe:
                case ItemID.DeathbringerPickaxe:
                case ItemID.CobaltPickaxe:
                case ItemID.PalladiumPickaxe:
                case ItemID.MythrilPickaxe:
                case ItemID.OrichalcumPickaxe:
                case ItemID.AdamantitePickaxe:
                case ItemID.TitaniumPickaxe:
                case ItemID.PickaxeAxe:
                case ItemID.Drax:
                case ItemID.SpectrePickaxe:
                case ItemID.ChlorophytePickaxe:
                case ItemID.ShroomiteDiggingClaw:
                case ItemID.Picksaw:

                case ItemID.CopperAxe:
                case ItemID.TinAxe:
                case ItemID.IronAxe:
                case ItemID.LeadAxe:
                case ItemID.SilverAxe:
                case ItemID.TungstenAxe:
                case ItemID.GoldAxe:
                case ItemID.PlatinumAxe:
                case ItemID.BloodLustCluster:
                case ItemID.WarAxeoftheNight:
                case ItemID.CobaltWaraxe:
                case ItemID.PalladiumWaraxe:
                case ItemID.MythrilWaraxe:
                case ItemID.OrichalcumWaraxe:
                case ItemID.AdamantiteWaraxe:
                case ItemID.TitaniumWaraxe:
                case ItemID.SpectreHamaxe:
                case ItemID.ChlorophyteGreataxe:
                    MetalWeapon.Add(type);
                    break;

                case ItemID.ReaverShark:
                case ItemID.SawtoothShark:
                    IceWeapon.Add(type);
                    break;

                case ItemID.CactusPickaxe:
                    PoisonWeapon.Add(type);
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
                    PlasmaWeapon.Add(type);
                    MetalWeapon.Add(type);
                    break;
            }
        }
    }
}
