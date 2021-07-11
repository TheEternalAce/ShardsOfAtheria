using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
    public class AbyssClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The weapon that once belonged an ancient abyssal king'");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 500;
            item.melee = true;
            item.crit = 16;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void HoldItem(Player player)
        {
            player.statLifeMax2 = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DemonClaw>());
            recipe.AddIngredient(ModContent.ItemType<AreusDagger>());
            recipe.AddIngredient(ModContent.ItemType<KitchenKnife>());
            recipe.AddIngredient(ModContent.ItemType<CrossDagger>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}