using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class PlumeCodex : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(7);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;

            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 2f;
            Item.crit = 6;
            Item.mana = 10;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shootSpeed = 20f;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 1, 75);
            Item.shoot = ModContent.ProjectileType<HardlightFeatherMagic>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<HardlightPrism>(15)
                .AddTile(TileID.SkyMill)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 vel = Main.MouseWorld + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 120f;
                    Projectile.NewProjectile(source, vel, Vector2.Normalize(Main.MouseWorld - vel) * Item.shootSpeed, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
    }
}