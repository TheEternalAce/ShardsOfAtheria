using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Summon.Active;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class DragonSpineWhip : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(26);
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(6, 8);
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.DefaultToWhip(ModContent.ProjectileType<DragonSpineWhipProj>(), 220, 2, 5, 26);
            Item.autoReuse = true;

            Item.rare = ItemDefaults.RaritySlayer;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentStardust, 32)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}