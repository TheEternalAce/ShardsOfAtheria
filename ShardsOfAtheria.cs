using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI;
using Terraria.UI;
using System.Collections.Generic;
using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.NPCs.NovaStellar;
using System;
using ShardsOfAtheria.Items.Weapons.Melee;

namespace ShardsOfAtheria
{
    public class ShardsOfAtheria : Mod
    {
        public static ModKeybind OverdriveKey;
        public static ModKeybind TomeKey;
        public static ModKeybind EmeraldTeleportKey;
        public static ModKeybind ShadowCloak;
        public static ModKeybind ShadowTeleport;
        public static ModKeybind PhaseSwitch;
        public static ModKeybind QuickCharge;
        public static ModKeybind SoulTeleport;

        public static ModKeybind QuickTest;

        public bool foundMod;
        public bool foundMod1;

        public override void Load()
        {
            OverdriveKey = KeybindLoader.RegisterKeybind(this, "Toggle Overdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(this, "Cycle Knowledge Base", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(this, "Emerald Teleport", "Z");
            ShadowCloak = KeybindLoader.RegisterKeybind(this, "Toggle Shadow Cloak", "OemTilde");
            ShadowTeleport = KeybindLoader.RegisterKeybind(this, "Shadow Teleport", "X");
            PhaseSwitch = KeybindLoader.RegisterKeybind(this, "Toggle Phase Type", "RightAlt");
            QuickCharge = KeybindLoader.RegisterKeybind(this, "Quick Charge", "C");
            SoulTeleport = KeybindLoader.RegisterKeybind(this, "Soul Crystal Teleport", "V");

            QuickTest = KeybindLoader.RegisterKeybind(this, "Quick Test (For mod developers, does nothing)", "OemComma");
        }

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("Census", out Mod foundMod))
                ModLoader.GetMod("Census").Call("TownNPCCondition", ModContent.NPCType<Atherian>(), "Defeat Eater of Worlds/Brain of Cthulhu while not in Slayer mode.");

            if (ModLoader.TryGetMod("BossChecklist", out Mod foundMod1))
            {
                ModLoader.GetMod("BossChecklist").Call(
                    "AddBoss",
                    this,
                    "Nova Stellar",
                    new List<int> { ModContent.NPCType<NovaStellar>() },
                    3.5f,
                    (() => SoAWorld.downedValkyrie),
                    () => true,
                    new List<int> { ModContent.ItemType<ValkyrieStormLance>(), ModContent.ItemType<GildedValkyrieWings>(), ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<ValkyrieCrown>(),
                        ItemID.GoldBar, ItemID.Feather },
                    ModContent.ItemType<ValkyrieCrest>(),
                    $"Use a [i:{ModContent.ItemType<ValkyrieCrest>()}] on the surface",
                    "Nova Stellar leaves in triumph"
                );
            }
        }
    }
}