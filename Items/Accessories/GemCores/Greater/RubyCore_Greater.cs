﻿using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class RubyCore_Greater : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddDamageType(7);
            Item.AddRedemptionElement(5);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityMechs;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RubyCore>())
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.LargeRuby)
                .AddIngredient(ItemID.FeralClaws)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().rubyGauntlet = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<RubyCore>().UpdateAccessory(player, hideVisual);
            player.GetAttackSpeed(DamageClass.Generic) += .04f;
            player.Gem().greaterRubyCore = true;
            player.Gem().greaterGemCore = true;
        }
    }
}