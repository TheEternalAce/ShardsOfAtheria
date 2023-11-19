using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Ranged.VergilFlamethrower;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusFlamethrower : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElementFire();
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 28;

            Item.damage = 90;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 12;

            Item.useTime = 20;
            Item.useLimitPerAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 10000;
            Item.shoot = ModContent.ProjectileType<ApproachingStorm>();
            Item.useAmmo = AmmoID.Gel;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return base.CanConsumeAmmo(ammo, player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(14)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddIngredient(ItemID.LunarBar, 15)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
