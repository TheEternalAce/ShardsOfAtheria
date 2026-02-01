using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class PowerTrip : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.BNEEnabled;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddDamageType(0, 2, 3, 5);
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;

            Item.damage = 42;
            Item.knockBack = 6;
            Item.crit = 2;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().powerTrip = true;
            player.Shards().areusProcessor = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ModContent.GetInstance<AreusProcessor>().ModifyTooltips(tooltips);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AcidTrip>())
                .AddIngredient(ModContent.ItemType<AreusProcessor>())
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
