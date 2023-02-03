using ShardsOfAtheria.Utilities;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class WandOfHealing : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.healLife = 50;
            Item.mana = 100;

            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item29;

            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 1);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ShardsRecipes.Gold, 3)
                .AddRecipeGroup(RecipeGroupID.IronBar, 6)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.ManaCrystal, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ShardsOfAtheria().heartBreak) return false;
            else return true;
        }

        public override void UpdateInventory(Player player)
        {
            player.ShardsOfAtheria().healingItem = true;
        }
    }
}