using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Biochemical
{
    public class BiochemicalToxicFlask : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ToxicFlask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Flask");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ToxicFlask);
            Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
        }

        public override void AddRecipes()
        {
            if (ModContent.GetInstance<ServerSideConfig>().biochemicalToxicFlask)
                CreateRecipe(1)
                    .AddIngredient(ItemID.ToxicFlask)
                    .AddTile(TileID.WorkBenches)
                    .Register();
        }
    }
}