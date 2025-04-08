using Microsoft.Xna.Framework.Input;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Items.Accessories.GemCores.Super;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
    public class MegaGemCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(0, 7);
            Item.AddRedemptionElement(5);
            Item.AddRedemptionElement(10);
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
                .AddIngredient(ModContent.ItemType<AmberCore_Super>())
                .AddIngredient(ModContent.ItemType<AmethystCore_Super>())
                .AddIngredient(ModContent.ItemType<DiamondCore_Super>())
                .AddIngredient(ModContent.ItemType<EmeraldCore_Super>())
                .AddIngredient(ModContent.ItemType<RubyCore_Super>())
                .AddIngredient(ModContent.ItemType<SapphireCore_Super>())
                .AddIngredient(ModContent.ItemType<TopazCore_Super>())
                .AddIngredient(ItemID.LunarBar, 20)
                .AddIngredient(ItemID.FragmentNebula, 10)
                .AddIngredient(ItemID.FragmentSolar, 10)
                .AddIngredient(ItemID.FragmentStardust, 10)
                .AddIngredient(ItemID.FragmentVortex, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            var gem = player.Gem();
            int loadout = player.CurrentLoadoutIndex;
            gem.masterCoreUI = true;
            gem.amberCape = gem.masterGemCoreToggles[loadout, 0];
            gem.amethystMask = gem.masterGemCoreToggles[loadout, 1];
            gem.diamondShield = gem.masterGemCoreToggles[loadout, 2];
            gem.emeraldBoots = gem.masterGemCoreToggles[loadout, 3];
            gem.rubyGauntlet = gem.masterGemCoreToggles[loadout, 4];
            gem.gemSoul = gem.sapphireSpirit = gem.masterGemCoreToggles[loadout, 5];
            gem.topazNecklace = gem.masterGemCoreToggles[loadout, 6];
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
            gem.masterCoreUI = !hideVisual;
            player.GetDamage(DamageClass.Generic) += .05f;
            player.GetAttackSpeed(DamageClass.Generic) += .04f;
            gem.sapphireDodgeChance += 0.05f;
            player.maxMinions++;
            player.statLifeMax2 += 20;
            player.moveSpeed += 0.05f;
            player.Gem().maxAmberBanners += 5;

            if (!hideVisual)
            {
                int loadout = player.CurrentLoadoutIndex;
                gem.amberCape = gem.masterGemCoreToggles[loadout, 0];
                gem.amethystMask = gem.masterGemCoreToggles[loadout, 1];
                gem.diamondShield = gem.masterGemCoreToggles[loadout, 2];
                gem.emeraldBoots = gem.masterGemCoreToggles[loadout, 3];
                gem.rubyGauntlet = gem.masterGemCoreToggles[loadout, 4];
                gem.gemSoul = gem.sapphireSpirit = gem.masterGemCoreToggles[loadout, 5];
                gem.topazNecklace = gem.masterGemCoreToggles[loadout, 6];
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string holdShiftText = ShardsHelpers.LocalizeCommon("HoldShiftTooltip");
            TooltipLine line = new(Mod, "HoldShift", holdShiftText);
            if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
            {
                line = this.ShiftTooltipCycle(7);
            }
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
        }
    }
}