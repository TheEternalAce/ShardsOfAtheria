using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ShardsOfAtheria.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class OrangeMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases ranged damage by 10% and crit chance by 4%\n" +
                "7 defense");
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Orange;
            Item.vanity = true;
        }

        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<SMPlayer>().OrangeMask = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 2)
                .AddIngredient(ItemID.Pumpkin, 5)
                .AddTile(TileID.Loom)
                .Register();
        }
    }
}
