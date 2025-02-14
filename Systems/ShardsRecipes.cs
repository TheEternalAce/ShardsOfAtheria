﻿using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
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
        public static RecipeGroup Tombstone;

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
            Bullet = null;
            Arrow = null;
            Rocket = null;
            Tombstone = null;
        }

        public override void AddRecipeGroups()
        {
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

            Tombstone = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Gravestone",
                   ItemID.Tombstone, ItemID.Headstone, ItemID.GraveMarker, ItemID.Gravestone, ItemID.CrossGraveMarker, ItemID.Obelisk, ItemID.RichGravestone1,
                   ItemID.RichGravestone2, ItemID.RichGravestone3, ItemID.RichGravestone4, ItemID.RichGravestone5);
            RecipeGroup.RegisterGroup("Shards:Gravestone", Tombstone);
        }

        public override void AddRecipes()
        {
            foreach (UpgradeBlueprint blueprint in UpgradeBlueprintLoader.upgrades)
            {
                blueprint.CreateRecipe();
            }

            Recipe.Create(ItemID.SoulofLight, 2)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.SoulofNight, 2)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.TerraBlade)
                .AddIngredient(ItemID.TrueNightsEdge)
                .AddIngredient(ItemID.TrueExcalibur)
                .AddIngredient(ModContent.ItemType<HeroSword>())
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
    }
}
