using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Projectiles.Weapon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Biochemical
{
    public class NeedleGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inject harmful bacteria into your opponent's system");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 26;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 10;
            Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
            Item.crit = 4;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(silver: 5, copper: 27);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.useAmmo = ModContent.ItemType<BacteriaNeedle>();
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 15f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 14)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}