using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Summon.Whip;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    public class DragonSpineWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("26 summon tag damage\n" +
                "Your summons will focus struck enemies");

            SoAGlobalItem.SlayerItem.Add(Type);

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.DefaultToWhip(ModContent.ProjectileType<DragonSpineWhipProj>(), 220, 2, 5, 26);
            
            Item.rare = ItemRarityID.Yellow;
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