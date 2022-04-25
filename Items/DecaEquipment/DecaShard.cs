using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A shard of immense power\n" +
                "Use to send it out to follow you");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Red;
            Item.width = 36;
            Item.height = 36;
            Item.maxStack = 999;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player) => player.GetModPlayer<DecaPlayer>().modelDeca;

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<DecaPlayer>().decaShards += 1;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(SoARecipes.DecaWeapon)
                .Register();
        }
    }
}