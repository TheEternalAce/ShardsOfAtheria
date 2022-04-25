using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.DecaEquipment;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
    public class SoARecipes : ModSystem
    {
        // A place to store the recipe group so we can easily use it later
        public static RecipeGroup EvilMaterial;
        public static RecipeGroup EvilGun;
        public static RecipeGroup Copper;
        public static RecipeGroup Silver;
        public static RecipeGroup Gold;
        public static RecipeGroup EvilBar;
        public static RecipeGroup Cobalt;
        public static RecipeGroup Mythril;
        public static RecipeGroup Adamantite;
        public static RecipeGroup Soul;
        public static RecipeGroup DecaWeapon;

        public override void Unload()
        {
            EvilMaterial = null;
            EvilGun = null;
            Copper = null;
            Silver = null;
            Gold = null;
            EvilBar = null;
            Cobalt = null;
            Mythril = null;
            Adamantite = null;
            Soul = null;
        }

        public override void AddRecipeGroups()
        {
            EvilMaterial = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Material",
                   ItemID.ShadowScale, ItemID.TissueSample);
            RecipeGroup.RegisterGroup("EvilMaterials", EvilMaterial);

            EvilGun = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Gun",
                   ItemID.Musket, ItemID.TheUndertaker);
            RecipeGroup.RegisterGroup("EvilGuns", EvilGun);

            Copper = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperBar)}",
                   ItemID.CopperBar, ItemID.TinBar);
            RecipeGroup.RegisterGroup("CopperBars", Copper);

            Silver = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}",
                   ItemID.SilverBar, ItemID.TungstenBar);
            RecipeGroup.RegisterGroup("SilverBars", Silver);

            Gold = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}",
                   ItemID.GoldBar, ItemID.PlatinumBar);
            RecipeGroup.RegisterGroup("GoldBars", Gold);

            EvilBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Bar",
                   ItemID.DemoniteBar, ItemID.CrimtaneBar);
            RecipeGroup.RegisterGroup("EvilBars", EvilBar);

            Cobalt = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 1 Bar",
                   ItemID.CobaltBar, ItemID.PalladiumBar);
            RecipeGroup.RegisterGroup("Tier1Bars", Cobalt);

            Mythril = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 2 Bar",
                   ItemID.MythrilBar, ItemID.OrichalcumBar);
            RecipeGroup.RegisterGroup("Tier2Bars", Mythril);

            Adamantite = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 3 Bar",
                   ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("Tier3Bars", Adamantite);

            Soul = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Soul",
                   ItemID.SoulofFlight, ItemID.SoulofFright, ItemID.SoulofLight, ItemID.SoulofMight, ItemID.SoulofNight, ItemID.SoulofSight, ModContent.ItemType<SoulOfDaylight>(),
                   ModContent.ItemType<SoulOfStarlight>(), ModContent.ItemType<SoulOfSpite>());
            RecipeGroup.RegisterGroup("Souls", Soul);

            DecaWeapon = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Deca Weapon",
                   ModContent.ItemType<DecaBow>(), ModContent.ItemType<DecaClaw>(), ModContent.ItemType<DecaRifle>(), ModContent.ItemType<DecaSaber>(),
                   ModContent.ItemType<DecaShotgun>(), ModContent.ItemType<DecaStaff>());
            RecipeGroup.RegisterGroup("DecaWeapon", DecaWeapon);
        }

        public override void AddRecipes()
        {
            Mod.CreateRecipe(ItemID.SoulofLight, 2)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Mod.CreateRecipe(ItemID.SoulofNight, 2)
                .AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Mod.CreateRecipe(ItemID.TerraBlade)
                .AddIngredient(ModContent.ItemType<HeroSword>())
                .AddIngredient(ItemID.TrueNightsEdge)
                .AddIngredient(ItemID.TrueExcalibur)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Mod.CreateRecipe(ItemID.LifeCrystal)
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 5)
                .AddIngredient(ItemID.Ruby, 5)
                .AddTile(TileID.Anvils)
                .Register();
            Mod.CreateRecipe(ItemID.LifeFruit)
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 5)
                .AddIngredient(ItemID.JungleSpores, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Mod.CreateRecipe(ItemID.GuideVoodooDoll)
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 5)
                .AddIngredient(ItemID.Silk, 5)
                .AddRecipeGroup(SoARecipes.Soul, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
            Mod.CreateRecipe(ItemID.ClothierVoodooDoll)
                .AddIngredient(ItemID.GuideVoodooDoll)
                .AddIngredient(ItemID.RedHat)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
