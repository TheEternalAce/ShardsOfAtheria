using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class PlagueCell : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = 1000;
            Item.ammo = Type;
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient<BionicBarItem>(5)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient(ItemID.Glass, 5)
                .AddIngredient<SoulOfSpite>(20)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}