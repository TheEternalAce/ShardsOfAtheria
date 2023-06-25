﻿using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
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

            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 3);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RubyCore_Greater>())
                .AddIngredient(ItemID.FragmentSolar, 5)
                .AddIngredient(ItemID.FragmentNebula, 5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().rubyGauntlet = !hideVisual;
            player.GetDamage(DamageClass.Generic) += .15f;
            player.Shards().superRubyCore = true;
            player.GetAttackSpeed(DamageClass.Generic) += .15f;
            player.GetKnockback(DamageClass.Generic) += 1.5f;
            player.meleeScaleGlove = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Weak] = true;
        }
    }
}