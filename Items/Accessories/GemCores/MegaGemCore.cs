using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Items.Accessories.GemCores.Super;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
    public class MegaGemCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.defense = 25;

            Item.rare = ItemDefaults.RarityLunarPillars;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AmethystCore_Super>())
                .AddIngredient(ModContent.ItemType<DiamondCore_Super>())
                .AddIngredient(ModContent.ItemType<EmeraldCore_Super>())
                .AddIngredient(ModContent.ItemType<RubyCore_Super>())
                .AddIngredient(ModContent.ItemType<SapphireCore_Super>())
                .AddIngredient(ModContent.ItemType<TopazCore_Super>())
                .AddIngredient(ItemID.Amber, 24)
                .AddIngredient(ItemID.LunarBar, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            var gem = player.Gem();
            int loadout = player.CurrentLoadoutIndex;
            gem.amberCape = gem.megaGemCoreToggles[loadout, 0];
            gem.amethystMask = gem.megaGemCoreToggles[loadout, 1];
            gem.diamondShield = gem.megaGemCoreToggles[loadout, 2];
            gem.emeraldBoots = gem.megaGemCoreToggles[loadout, 3];
            gem.rubyGauntlet = gem.megaGemCoreToggles[loadout, 4];
            gem.sapphireSpirit = gem.megaGemCoreToggles[loadout, 5];
            gem.topazNecklace = gem.megaGemCoreToggles[loadout, 6];
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var gem = player.Gem();
            AmethystDashPlayer mp = player.GetModPlayer<AmethystDashPlayer>();
            mp.DashVelocity = 20f;

            ModContent.GetInstance<AmberCore_Super>().UpdateAccessory(player, true);
            ModContent.GetInstance<AmethystCore_Super>().UpdateAccessory(player, true);
            ModContent.GetInstance<DiamondCore_Super>().UpdateAccessory(player, true);
            ModContent.GetInstance<EmeraldCore_Super>().UpdateAccessory(player, true);
            ModContent.GetInstance<RubyCore_Super>().UpdateAccessory(player, true);
            ModContent.GetInstance<SapphireCore_Super>().UpdateAccessory(player, true);
            ModContent.GetInstance<TopazCore_Super>().UpdateAccessory(player, true);

            gem.megaGemCore = true;
            player.GetDamage(DamageClass.Generic) += .05f;
            player.GetAttackSpeed(DamageClass.Generic) += .05f;
            gem.sapphireDodgeChance += 0.05f;
            player.maxMinions++;
            player.statLifeMax2 += 20;

            if (!hideVisual)
            {
                int loadout = player.CurrentLoadoutIndex;
                gem.amberCape = gem.megaGemCoreToggles[loadout, 0];
                gem.amethystMask = gem.megaGemCoreToggles[loadout, 1];
                gem.diamondShield = gem.megaGemCoreToggles[loadout, 2];
                gem.emeraldBoots = gem.megaGemCoreToggles[loadout, 3];
                gem.rubyGauntlet = gem.megaGemCoreToggles[loadout, 4];
                gem.sapphireSpirit = gem.megaGemCoreToggles[loadout, 5];
                gem.topazNecklace = gem.megaGemCoreToggles[loadout, 6];
            }
        }
    }
}