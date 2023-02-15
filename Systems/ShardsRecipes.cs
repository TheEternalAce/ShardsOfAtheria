using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Systems
{
    public class ShardsRecipes : ModSystem
    {
        // A place to store the recipe group so we can easily use it later
        public static RecipeGroup EvilMaterial;
        public static RecipeGroup Copper;
        public static RecipeGroup Silver;
        public static RecipeGroup Gold;
        public static RecipeGroup EvilBar;
        public static RecipeGroup Cobalt;
        public static RecipeGroup Mythril;
        public static RecipeGroup Adamantite;
        public static RecipeGroup Soul;
        public static RecipeGroup HMAnvil;

        public static RecipeGroup Bullet;
        public static RecipeGroup Arrow;
        public static RecipeGroup Rocket;

        public override void Unload()
        {
            EvilMaterial = null;
            Copper = null;
            Silver = null;
            Gold = null;
            EvilBar = null;
            Cobalt = null;
            Mythril = null;
            Adamantite = null;
            Soul = null;
            HMAnvil = null;
        }

        public override void AddRecipeGroups()
        {
            AddAmmoToLists();

            EvilMaterial = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Material",
                   ItemID.ShadowScale, ItemID.TissueSample);
            RecipeGroup.RegisterGroup("Shards:EvilMaterials", EvilMaterial);

            Copper = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperBar)}",
                   ItemID.CopperBar, ItemID.TinBar);
            RecipeGroup.RegisterGroup("Shards:CopperBars", Copper);

            Silver = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}",
                   ItemID.SilverBar, ItemID.TungstenBar);
            RecipeGroup.RegisterGroup("Shards:SilverBars", Silver);

            Gold = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}",
                   ItemID.GoldBar, ItemID.PlatinumBar);
            RecipeGroup.RegisterGroup("Shards:GoldBars", Gold);

            EvilBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Bar",
                   ItemID.DemoniteBar, ItemID.CrimtaneBar);
            RecipeGroup.RegisterGroup("Shards:EvilBars", EvilBar);

            Cobalt = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 1 Bar",
                   ItemID.CobaltBar, ItemID.PalladiumBar);
            RecipeGroup.RegisterGroup("Shards:Tier1Bars", Cobalt);

            Mythril = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 2 Bar",
                   ItemID.MythrilBar, ItemID.OrichalcumBar);
            RecipeGroup.RegisterGroup("Shards:Tier2Bars", Mythril);

            Adamantite = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 3 Bar",
                   ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("Shards:Tier3Bars", Adamantite);

            Soul = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Soul",
                   ItemID.SoulofFlight, ItemID.SoulofFright, ItemID.SoulofLight, ItemID.SoulofMight, ItemID.SoulofNight, ItemID.SoulofSight, ModContent.ItemType<SoulOfDaylight>(),
                   ModContent.ItemType<SoulOfTwilight>(), ModContent.ItemType<SoulOfSpite>());
            RecipeGroup.RegisterGroup("Shards:Souls", Soul);

            List<int> arrows = new();
            arrows.AddRange(SoAGlobalItem.preHardmodeArrows);
            arrows.AddRange(SoAGlobalItem.hardmodeArrows);
            arrows.AddRange(SoAGlobalItem.postMoonLordArrows);

            Arrow = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} arrow",
                   arrows.ToArray());
            RecipeGroup.RegisterGroup("Shards:Arrows", Arrow);

            List<int> bullets = new();
            bullets.AddRange(SoAGlobalItem.preHardmodeBullets);
            bullets.AddRange(SoAGlobalItem.hardmodeBullets);
            bullets.AddRange(SoAGlobalItem.postMoonLordBullets);
            Bullet = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} bullet",
                   bullets.ToArray());
            RecipeGroup.RegisterGroup("Shards:Bullets", Bullet);

            List<int> rockets = new();
            rockets.AddRange(SoAGlobalItem.preHardmodeRockets);
            rockets.AddRange(SoAGlobalItem.hardmodeRockets);
            rockets.AddRange(SoAGlobalItem.postMoonLordRockets);

            Rocket = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} rocket",
                   rockets.ToArray());
            RecipeGroup.RegisterGroup("Shards:Rockets", Rocket);

            if (ModLoader.TryGetMod("MagicStorage", out Mod magicStorage))
            {
                HMAnvil = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.MythrilAnvil)}\"",
                       ModContent.ItemType<CobaltWorkbenchItem>(), ModContent.ItemType<PalladiumWorkbenchItem>());
                RecipeGroup.RegisterGroup("MagicStorage:AnyHmAnvil", HMAnvil);
            }
        }

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.SoulofLight, 2)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.SoulofNight, 2)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.TerraBlade)
                .AddIngredient(ModContent.ItemType<HeroSword>())
                .AddIngredient(ItemID.TrueNightsEdge)
                .AddIngredient(ItemID.TrueExcalibur)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.GuideVoodooDoll)
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 5)
                .AddIngredient(ItemID.Silk, 5)
                .AddRecipeGroup(Soul, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
            Recipe.Create(ItemID.ClothierVoodooDoll)
                .AddIngredient(ItemID.GuideVoodooDoll)
                .AddIngredient(ItemID.RedHat)
                .AddTile(TileID.DemonAltar)
                .Register();

            Recipe.Create(ItemID.RodofDiscord)
                .AddIngredient(ItemID.Teleporter, 20)
                .AddIngredient(ItemID.HallowedBar, 20)
                .AddIngredient(ItemID.BeetleHusk, 18)
                .AddIngredient(ItemID.SoulofFlight, 14)
                .AddIngredient(ItemID.SoulofLight, 14)
                .AddIngredient(ItemID.ChaosFish, 4)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void PostAddRecipes()
        {
            for (var i = 0; i < Recipe.maxRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if ((recipe.TryGetIngredient(ItemID.Bottle, out Item _) || recipe.TryGetIngredient(ItemID.BottledWater, out Item _) || recipe.TryGetIngredient(ItemID.BottledHoney, out Item _))
                    && recipe.HasTile(TileID.Bottles) && recipe.createItem.buffTime > 0)
                {
                    SoAGlobalItem.Potions.Add(recipe.createItem.type);
                }
            }
        }

        void AddAmmoToLists()
        {
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
                        if (item.rare < ItemRarityID.LightRed && item.rare != ItemRarityID.Expert && item.rare != ItemRarityID.Master)
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
                        if (item.rare < ItemRarityID.LightRed && item.rare != ItemRarityID.Expert && item.rare != ItemRarityID.Master)
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
                        if (item.rare < ItemRarityID.LightRed && item.rare != ItemRarityID.Expert && item.rare != ItemRarityID.Master)
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
            }
        }
    }
}
