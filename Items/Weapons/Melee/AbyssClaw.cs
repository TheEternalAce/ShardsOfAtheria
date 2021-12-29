using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AbyssClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The weapon that once belonged an ancient abyssal king'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 500;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 16;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void HoldItem(Player player)
        {
            player.statLifeMax2 = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DemonClaw>())
                .AddIngredient(ModContent.ItemType<AreusDagger>())
                .AddIngredient(ModContent.ItemType<KitchenKnife>())
                .AddIngredient(ModContent.ItemType<CrossDagger>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}