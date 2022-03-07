using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class WandOfHealing : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Converts Mana into Life'\n" +
                "Taking damage while this is in your inventory will render this unusable for a time");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.mana = 100;
            Item.healLife = 50;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(gold: 5, silver: 70);
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item29;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(SoARecipes.Gold, 3)
                .AddRecipeGroup(RecipeGroupID.IronBar, 6)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.ManaCrystal, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<SoAPlayer>().heartBreak) return false;
            else return true;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SoAPlayer>().sMHealingItem = true;
        }
    }
}