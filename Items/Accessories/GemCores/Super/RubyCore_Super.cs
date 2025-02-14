using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class RubyCore_Super : ModItem
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

            Item.rare = ItemDefaults.RarityLunaticCultist;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RubyCore_Greater>())
                .AddIngredient(ItemID.BeetleHusk, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().rubyGauntlet = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<RubyCore_Greater>().UpdateAccessory(player, hideVisual);
            player.Gem().superRubyCore = true;
            player.GetDamage(DamageClass.Generic) += .05f;
            player.GetAttackSpeed(DamageClass.Generic) += .04f;
            player.Gem().superGemCore = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;
            var gem = player.Gem();
            if (gem.rubyCore && gem.sapphireCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Sapphire Curse") { OverrideColor = Color.Red };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
            if (gem.rubyCore && gem.diamondCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Ruby Curse") { OverrideColor = Color.Cyan };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
                line = new(Mod, "GemCurse", "Diamond Curse") { OverrideColor = Color.Red };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
            if (gem.rubyCore && gem.emeraldCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Emerald Curse") { OverrideColor = Color.Red };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
        }
    }
}