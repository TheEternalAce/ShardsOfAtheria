using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon.Minion;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.NPCs.NovaStellar;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
    public partial class ShardsOfAtheria : Mod
    {
        public static ModKeybind OverdriveKey;
        public static ModKeybind TomeKey;
        public static ModKeybind EmeraldTeleportKey;
        public static ModKeybind PhaseSwitch;
        public static ModKeybind QuickCharge;
        public static ModKeybind SoulTeleport;
        public static ModKeybind ArmorSetBonusActive;

        public static ModKeybind QuickTest;

        public override void Load()
        {
            OverdriveKey = KeybindLoader.RegisterKeybind(this, "Toggle Overdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(this, "Cycle Knowledge Base", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(this, "Emerald Teleport", "Z");
            PhaseSwitch = KeybindLoader.RegisterKeybind(this, "Toggle Phase Type", "RightAlt");
            QuickCharge = KeybindLoader.RegisterKeybind(this, "Quick Charge", "C");
            SoulTeleport = KeybindLoader.RegisterKeybind(this, "Soul Crystal Teleport", "V");
            ArmorSetBonusActive = KeybindLoader.RegisterKeybind(this, "Activate Armor Set Bonus", "Mouse4");

            QuickTest = KeybindLoader.RegisterKeybind(this, "Quick Test (For mod developers, does nothing)", "OemComma");
        }

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("Census", out Mod foundMod))
            {
                foundMod.Call("TownNPCCondition", ModContent.NPCType<Atherian>(), "Defeat Eater of Worlds/Brain of Cthulhu while not in Slayer mode.");
            }

            if (ModLoader.TryGetMod("BossChecklist", out Mod foundMod1))
            {
                foundMod1.Call(
                    "AddBoss",
                    this,
                    "Nova Stellar",
                    new List<int> { ModContent.NPCType<NovaStellar>() },
                    3.5f,
                    () => SoADownedSystem.downedValkyrie,
                    () => true,
                    new List<int> { ModContent.ItemType<ValkyrieStormLance>(), ModContent.ItemType<GildedValkyrieWings>(), ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<DownBow>(),
                        ModContent.ItemType<PlumeCodex>(), ModContent.ItemType<NestlingStaff>(), ModContent.ItemType<ValkyrieCrown>(),
                        ItemID.GoldBar, ItemID.Feather },
                    ModContent.ItemType<ValkyrieCrest>(),
                    $"Use a [i:{ModContent.ItemType<ValkyrieCrest>()}] on the surface",
                    "Nova Stellar leaves in triumph"
                );
            }
            if (ModLoader.TryGetMod("Fargowiltas", out Mod foundMod2))
            {
                foundMod2.Call("AddSummon", 5.5f, ModContent.ItemType<ValkyrieCrest>(), () => SoADownedSystem.downedValkyrie, 50000);
            }
        }
    }
}