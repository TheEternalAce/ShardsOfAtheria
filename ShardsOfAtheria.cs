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

            if (!Main.dedServ)
            {
                /*
                if (ModContent.GetInstance<Config>().MegamergeVisual)
                {
                    AddEquipTexture(new Items.BiometalHead(), null, EquipType.Head, "OmegaMetalHead", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Head");
                    AddEquipTexture(new Items.BiometalBody(), null, EquipType.Body, "OmegaMetalBody", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Body", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Arms");
                    AddEquipTexture(new Items.BiometalLegs(), null, EquipType.Legs, "OmegaMetalLegs", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Legs");

                    AddEquipTexture(new Items.BiometalHead(), null, EquipType.Head, "BiometalHead", "ShardsOfAtheria/Items/Biometal_Head");
                    AddEquipTexture(new Items.BiometalBody(), null, EquipType.Body, "BiometalBody", "ShardsOfAtheria/Items/Biometal_Body", "ShardsOfAtheria/Items/Biometal_Arms");
                    AddEquipTexture(new Items.BiometalLegs(), null, EquipType.Legs, "BiometalLegs", "ShardsOfAtheria/Items/Biometal_Legs");
                }
                */
                //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicName"), ModContent.ItemType<MusicBox>(), ModContent.TileType<MusicBoxTile>());
            }
        }

        public override void PostSetupContent()
        {
            List<int> DeathItemList = new List<int>(){
                ModContent.ItemType<BloodScythe>()
            };
            List<int> NovaItemList = new List<int>(){
                ModContent.ItemType<GildedValkyrieWings>(),
                ItemID.Feather,
                ItemID.GoldBar,
                ModContent.ItemType<ValkyrieCrown>(),
                ModContent.ItemType<ValkyrieBlade>(),
                ModContent.ItemType<ValkyrieStormLance>()
            };
            if (ModLoader.TryGetMod("Census", out Mod foundMod))
                ModLoader.GetMod("Census").Call("TownNPCCondition", ModContent.NPCType<Atherian>(), "Defeat Eater of Worlds/Brain of Cthulhu.");

            if (ModLoader.TryGetMod("Census", out Mod foundMod1))
            {
                ModLoader.GetMod("BossChecklist").Call(
                    "AddBoss",
                    3.5f,
                    new List<int> { ModContent.NPCType<NovaStellar>() },
                    this, // Mod
                    "Nova Stellar",
                    (Func<bool>)(() => SoAWorld.downedValkyrie),
                    ModContent.ItemType<ValkyrieCrest>(),
                    new List<int> { ModContent.ItemType<ValkyrieStormLance>(), ModContent.ItemType<GildedValkyrieWings>() },
                    new List<int> { ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<ValkyrieCrown>(), ItemID.GoldBar, ItemID.Feather },
                    $"Use a [i:{ModContent.ItemType<ValkyrieCrest>()}] on the surface"
                );
                /*
                bossChecklist.Call(
                    "AddBoss",
                    15.5f,
                    ModContent.NPCType<Death>(),
                    this,
                    "Death",
                    (Func<bool>)(() => SoAWorld.downedDeath),
                    ModContent.ItemType<AncientCoin>(),
                    new List<int> { ModContent.ItemType<DeathEssence>(), ModContent.ItemType<Items.Armor.BunnyMask>(), ModContent.ItemType<Items.Placeable.PuritySpiritTrophy>(), ModContent.ItemType<Items.Placeable.BunnyTrophy>(), ModContent.ItemType<Items.Placeable.TreeTrophy>() },
                    new List<int> { ModContent.ItemType<Items.PurityShield>(), ItemID.Bunny },
                    $"Use a [i:{ModContent.ItemType<AncientMedallion>()}] anywhere"
                );
                */
            }
        }
    }
}