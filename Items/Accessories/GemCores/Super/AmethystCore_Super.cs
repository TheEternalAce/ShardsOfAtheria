using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class AmethystCore_Super : ModItem
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
                .AddIngredient(ModContent.ItemType<AmethystCore_Greater>())
                .AddIngredient(ItemID.BeetleHusk, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().amethystMask = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Gem().superAmethystCore = true;
            AmethystDashPlayer mp = player.GetModPlayer<AmethystDashPlayer>();
            mp.DashVelocity = 13f;
            ModContent.GetInstance<AmethystCore_Greater>().UpdateAccessory(player, hideVisual);
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frostburn2] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[ModContent.BuffType<ElectricShock>()] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
        }
    }
}