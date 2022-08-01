using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    [AutoloadEquip(EquipType.Legs)]
    public class EntropicLeggings : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("8% increased damage and critical strike chance\n" +
                "10% increased movement speed");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
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