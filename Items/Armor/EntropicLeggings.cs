using ShardsOfAtheria.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class EntropicLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 3, 50, 0);
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .08f;
            player.GetCritChance(DamageClass.Generic) += .08f;
            player.moveSpeed += 0.1f; // Increase the movement speed of the player
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.LunarBar, 24)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}