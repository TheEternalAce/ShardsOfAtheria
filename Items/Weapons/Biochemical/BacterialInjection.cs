using ShardsOfAtheria.Projectiles.Weapon.Biochemical;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Biochemical
{
    public class BacterialInjection : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inject harmful bacteria into your opponent's system");
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 62;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.damage = 14;
            Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
            Item.crit = 4;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(silver: 5, copper: 27);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<BacterialInjectionProj>();
            Item.shootSpeed = 2.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 6)
                .AddIngredient(ItemID.Glass, 6)
                .AddIngredient(ModContent.ItemType<Bacteria>(), 50)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}