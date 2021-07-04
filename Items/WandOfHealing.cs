using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
    public class WandOfHealing : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.mana = 100;
            item.healLife = 50;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.value = Item.sellPrice(gold: 5, silver: 70);
            item.rare = ItemRarityID.Red;
            item.autoReuse = false;
            item.UseSound = SoundID.Item29;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("SM:GoldBars", 3);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 6);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<SMPlayer>().heartBreak) return false;
            else return true;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SMPlayer>().sMHealingItem = true;
        }
    }
}