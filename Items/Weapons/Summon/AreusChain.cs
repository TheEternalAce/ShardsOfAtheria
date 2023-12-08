using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Summon.Whip;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class AreusChain : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.DefaultToWhip(ModContent.ProjectileType<AreusChainWhip>(), 40, 2, 3.55f);

            Item.rare = ItemDefaults.RarityAreus;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(16)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.SoulofLight, 12)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}