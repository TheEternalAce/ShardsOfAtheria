using ShardsOfAtheria.Projectiles.Magic.Spectrum;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.MrGerd26
{
    // Bro owns the spectrum
    public class AlphaSpectrum : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;

            Item.AddDamageType(4);
            Item.AddElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 100;
            Item.height = 42;

            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.crit = 8;
            Item.mana = 20;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueDungeon;
            Item.shoot = ModContent.ProjectileType<GerdGun>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SpaceGun)
                .AddRecipeGroup(ShardsRecipes.Silver, 20)
                .AddIngredient(ItemID.Bone, 16)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
