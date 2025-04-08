using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BuffItems
{
    public class ChargedFlightPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 50;
            Item.maxStack = 9999;

            Item.DefaultToPotion(ModContent.BuffType<ChargedFlight>(), 28800);

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueBuffPotion;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HardlightPrism>())
                .AddIngredient(ModContent.ItemType<AreusShard>())
                .AddIngredient(ItemID.BottledWater)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }

    public class ChargedFlight : ModBuff
    {

    }

    public class ChargedFlightPlayer : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            if (Player.HasBuff(ModContent.BuffType<ChargedFlight>()))
            {
                Player.wingTimeMax += 34;
            }
        }
    }
}