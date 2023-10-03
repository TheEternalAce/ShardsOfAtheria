using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BuffItems
{
    [JITWhenModsEnabled("BattleNetworkElements")]
    public class InsulationPotion : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.ElementModEnabled;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 50;
            Item.maxStack = 9999;

            Item.DefaultToPotion(ModContent.BuffType<Insulation>(), 14400);

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueBuffPotion;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood)
                .AddIngredient(ItemID.Wire)
                .AddIngredient(ItemID.Leather)
                .AddIngredient(ItemID.BottledWater)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }

    [JITWhenModsEnabled("BattleNetworkElements")]
    public class Insulation : ModBuff
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.ElementModEnabled;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (SoA.ElementModEnabled)
            {
                player.ElecMultiplier() -= 0.2f;
            }
        }
    }
}