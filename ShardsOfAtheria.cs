using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Globals.Elements;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon.Minion;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.NPCs.Town;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
    public partial class ShardsOfAtheria : Mod
    {
        public static bool DeveloperMode = true;

        public static int MaxNecronomiconPages = 2;

        public static ModKeybind OverdriveKey;
        public static ModKeybind TomeKey;
        public static ModKeybind EmeraldTeleportKey;
        public static ModKeybind PhaseSwitch;
        public static ModKeybind SoulTeleport;
        public static ModKeybind ArmorSetBonusActive;

        public override void Load()
        {
            OverdriveKey = KeybindLoader.RegisterKeybind(this, "Toggle Overdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(this, "Cycle Knowledge Base", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(this, "Emerald Teleport", "Z");
            PhaseSwitch = KeybindLoader.RegisterKeybind(this, "Toggle Phase Type", "RightAlt");
            SoulTeleport = KeybindLoader.RegisterKeybind(this, "Soul Crystal Teleport", "V");
            ArmorSetBonusActive = KeybindLoader.RegisterKeybind(this, "Activate Armor Set Bonus", "Mouse4");
        }

        public override void PostSetupContent()
        {
            if (ModContent.GetInstance<ShardsConfigClientSide>().windowTitle)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        Main.instance.Window.Title = "Shards of Atheria: Splitting Skies";
                        break;
                }
            }

            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                int type = item.type;

                if (item.consumable)
                {
                    if (item.ammo > AmmoID.None)
                    {
                        if (item.rare < ItemRarityID.LightRed && item.rare != ItemRarityID.Expert && item.rare != ItemRarityID.Master)
                        {
                            SoAGlobalItem.preHardmodeAmmo.Add(type);
                        }
                        else if (item.rare < ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.hardmodeAmmo.Add(type);
                        }
                        else if (item.rare >= ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.postMoonLordAmmo.Add(type);
                        }
                    }
                    if (item.ammo == AmmoID.Arrow)
                    {
                        if (item.rare < ItemRarityID.LightRed && item.rare != ItemRarityID.Expert  && item.rare != ItemRarityID.Master)
                        {
                            SoAGlobalItem.preHardmodeArrows.Add(type);
                        }
                        else if (item.rare < ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.hardmodeArrows.Add(type);
                        }
                        else if (item.rare >= ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.postMoonLordArrows.Add(type);
                        }
                    }
                    if (item.ammo == AmmoID.Bullet)
                    {
                        if (item.rare < ItemRarityID.LightRed && item.rare != ItemRarityID.Expert  && item.rare != ItemRarityID.Master)
                        {
                            SoAGlobalItem.preHardmodeBullets.Add(type);
                        }
                        else if (item.rare < ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.hardmodeBullets.Add(type);
                        }
                        else if (item.rare >= ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.postMoonLordBullets.Add(type);
                        }
                    }
                    if (item.ammo == AmmoID.Rocket)
                    {
                        if (item.rare < ItemRarityID.LightRed && item.rare != ItemRarityID.Expert  && item.rare != ItemRarityID.Master)
                        {
                            SoAGlobalItem.preHardmodeRockets.Add(type);
                        }
                        else if (item.rare < ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.hardmodeRockets.Add(type);
                        }
                        else if (item.rare >= ItemRarityID.Cyan)
                        {
                            SoAGlobalItem.postMoonLordRockets.Add(type);
                        }
                    }
                }

                #region Assign Base-Elements weapon to branching Sub-Element


                if (type == ModContent.ItemType<TheMourningStar>())
                {
                    SoAGlobalItem.ElectricWeapon.Remove(type);
                }
                #endregion
            }

            for (int i = 0; i < ProjectileLoader.ProjectileCount; i++)
            {
                Projectile projectile = ContentSamples.ProjectilesByType[i];
                int type = projectile.type;

                #region Assign Base-Elements projectile to branching Sub-Element

                if (type == ModContent.ProjectileType<MourningStar>())
                {
                    ProjectileElements.ElectricProj.Remove(type);
                }
                #endregion
            }

            for (int i = 0; i < NPCLoader.NPCCount; i++)
            {
                NPC npc = ContentSamples.NpcsByNetId[i];
                int type = npc.type;

                #region Assign Base-Elements NPC to branching Sub-Element

                #endregion
            }

            #region Mod calls
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
                    5.5f,
                    () => ShardsDownedSystem.downedValkyrie,
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
                foundMod2.Call("AddSummon", 5.5f, ModContent.ItemType<ValkyrieCrest>(), () => ShardsDownedSystem.downedValkyrie, 50000);
            }

            if (ModLoader.TryGetMod("RORBossHealthbars", out Mod ror2HBS))
            {
                ror2HBS.Call("HPPool", new List<int>()
                {
                    ModContent.NPCType<NovaStellar>()
                });
                ror2HBS.Call("CustomName", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCName.NovaStellar");
                ror2HBS.Call("BossDesc", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.BossDesc.NovaStellar");
            }

            if (ModLoader.TryGetMod("RiskOfTerrain", out Mod rot))
            {
                rot.Call("HPPool", new List<int>()
                {
                    ModContent.NPCType<NovaStellar>()
                });
                rot.Call("CustomName", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCName.NovaStellar");
                rot.Call("BossDesc", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.BossDesc.NovaStellar");
            }
            #endregion
        }
    }
}