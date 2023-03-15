using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon.Minion;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.NPCs.Town;
using ShardsOfAtheria.Systems;
using ShopQuotesMod;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
    public partial class ShardsOfAtheriaMod : Mod
    {
        public static int MaxNecronomiconPages = 2;

        public static ModKeybind OverdriveKey;
        public static ModKeybind TomeKey;
        public static ModKeybind EmeraldTeleportKey;
        public static ModKeybind PhaseSwitch;
        public static ModKeybind SoulTeleport;
        public static ModKeybind ArmorSetBonusActive;

        public static ShardsServerConfig ServerConfig;
        public static ShardsClientConfig ClientConfig;

        public override void Load()
        {
            OverdriveKey = KeybindLoader.RegisterKeybind(this, "Toggle Overdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(this, "Cycle Knowledge Base", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(this, "Emerald Teleport", "Z");
            PhaseSwitch = KeybindLoader.RegisterKeybind(this, "Toggle Phase Type", "RightAlt");
            SoulTeleport = KeybindLoader.RegisterKeybind(this, "Soul Crystal Teleport", "V");
            ArmorSetBonusActive = KeybindLoader.RegisterKeybind(this, "Activate Armor Set Bonus", "Mouse4");

            ServerConfig = ModContent.GetInstance<ShardsServerConfig>();
            ClientConfig = ModContent.GetInstance<ShardsClientConfig>();

            ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            if (wikithis != null && !Main.dedServ)
            {
                wikithis.Call("AddModURL", this, "terrariamods.wiki.gg$Shards_of_Atheria");

                // You can also use call ID for some calls!
                //wikithis.Call(0, this, "https://terrariamods.wiki.gg$Shards_of_Atheria");

                // Alternatively, you can use this instead, if your wiki is on terrariamods.fandom.com
                //wikithis.Call(0, this, "https://terrariamods.fandom.com/wiki/Example_Mod/{}");
                //wikithis.Call("AddModURL", this, "https://terrariamods.fandom.com/wiki/Example_Mod/{}");

                // If there wiki on other languages (such as russian, spanish, chinese, etch), then you can also call that:
                //wikithis.Call(0, this, "https://examplemod.wiki.gg/zh/wiki/{}", GameCulture.CultureName.Chinese)

                // If you want to replace default icon for your mod, then call this. Icon should be 30x30, either way it will be cut.
                wikithis.Call("AddWikiTexture", this, ModContent.Request<Texture2D>("ShardsOfAtheria/icon_small"));
                //wikithis.Call(3, this, ModContent.Request<Texture2D>(pathToIcon));

                // If you want to add wiki entries to your custom element (for example, mod enchantments, buffs, etch, literally anything).
                // Example of adding wiki pages for projectiles:
                //wikithis.Call("CustomWiki",
                //    this, // instance of your mod
                //    "ProjectileWiki", // name of wiki
                //    new Func<object, IConvertible>(x => (x as Projectile).type), // type of your entry (can be anything)
                //    new Action<Func<object, bool>, Action<object, IConvertible, string>, Func<string, Mod, string>>((hasEntryFunc, addEntryFunc, defaultSearchStr) =>
                //    {
                //        foreach (Projectile proj in ContentSamples.ProjectilesByType.Values) // iterate through each instance
                //        {
                //                if (hasEntryFunc(proj.type)) // check if entry exists, and if it does, then skip
                //                    continue;
                //                
                //                // get projectile name
                //                string name = proj.type < ProjectileID.Count
                //                    ? Language.GetTextValue($"ProjectileName.{ProjectileID.Search.GetName(proj.type)}")
                //                    : Language.GetTextValue($"Mods.{proj.ModProjectile.Mod.Name}.ProjectileName.{proj.ModProjectile.Name}");
                //                
                //                addEntryFunc(proj, proj.type, defaultSearchStr(name, proj.ModProjectile?.Mod)); // add entry
                //        }
                //    }
                //
                // Whenever you want to open your custom wiki page, then you should use this mod call:
                //wikithis.Call("OpenCustomWiki",
                //    this, // instance of your mod
                //    "ProjectileWiki",
                //    (int)ProjectileID.AdamantiteChainsaw, // id of projectile. should match type of your entry (Projectile.type is int)
                //    true // forces check for keybind, you would most likely want to keep this as it is
                //    );
            }
        }

        public override void PostSetupContent()
        {
            if (ModContent.GetInstance<ShardsClientConfig>().windowTitle)
            {
                if (Main.rand.NextBool(3))
                {
                    Main.instance.Window.Title = ChooseTitleText(0);
                }
            }

            // Add Areus weapons to Electric element list
            foreach (int item in SoAGlobalItem.AreusWeapon)
            {
                WeaponElements.Electric.Add(item);
            }

            // Weak Shop Quotes reference
            if (ModLoader.HasMod("ShopQuotesMod"))
            {
                TryAddingShopQuotes();
            }

            // Mod calls
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
        }

        [JITWhenModsEnabled("ShopQuotesMod")]
        internal void TryAddingShopQuotes()
        {
            ModContent.GetInstance<QuoteDatabase>()
                .AddNPC(ModContent.NPCType<Atherian>(), this, "Mods.ShardsOfAtheria.ShopQuote.")
                .UseColor(Color.Cyan);
        }

        public string ChooseTitleText(int id = 0)
        {
            List<string> title = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                title.Add(Language.GetTextValue("Mods.ShardsOfAtheria.Common.TitleText" + (i + 1)));
            }
            int index = Main.rand.Next(3);

            return title[index];
        }
    }
}