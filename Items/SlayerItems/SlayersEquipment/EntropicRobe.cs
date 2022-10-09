using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    [AutoloadEquip(EquipType.Body)]
    public class EntropicRobe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("12% increased damage and critical strike chance");

            SoAGlobalItem.SlayerItem.Add(Type);

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.defense = 28;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .12f;
            player.GetCritChance(DamageClass.Generic) += .12f;
            player.moveSpeed += 0.05f; // Increase the movement speed of the player
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.LunarBar, 32)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}