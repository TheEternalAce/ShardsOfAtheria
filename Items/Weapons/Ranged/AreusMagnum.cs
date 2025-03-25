using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Ranged.AreusUltrakillGun;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusMagnum : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5, 7);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 26;

            Item.damage = 37;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.autoReuse = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 0, 25);
            Item.shoot = ModContent.ProjectileType<AreusMagnumProj>();
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 7)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AreusMagnumProj>()] > 0)
            {
                return base.CanConsumeAmmo(ammo, player);
            }
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = Item.shoot;
        }
    }
}