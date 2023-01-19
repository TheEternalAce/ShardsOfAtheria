using MMZeroElements;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Summon.Whip;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon.Whip
{
    public class DragonSpineWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Ice.Add(Type);
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