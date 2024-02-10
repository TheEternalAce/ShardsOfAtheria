using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public class OverdriveEnergyPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.NPCHit53;
            Item.useTurn = true;
            Item.consumable = true;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return player.Shards().overdriveTimeCurrent != ShardsPlayer.OVERDRIVE_TIME_MAX;
        }

        public override bool? UseItem(Player player)
        {
            player.Shards().overdriveTimeCurrent = 300;
            return true;
        }
    }
}
