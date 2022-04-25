using ShardsOfAtheria.Projectiles.Weapon.Biochemical;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Biochemical
{
    public class MicrobeScraper : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Scrape off microbes from your enemies");
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 7;
            Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
            Item.crit = 4;
            Item.knockBack = 0;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(silver: 5, copper: 27);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<MicrobeScraperProj>();
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.Glass)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}