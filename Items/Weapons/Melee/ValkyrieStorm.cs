using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ValkyrieStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Rains feather blades on your enemies'");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 67;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0,  10);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FeatherBladeFriendly>();
            Item.shootSpeed = 8;
        }

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ValkyrieBlade>())
				.AddIngredient(ItemID.SpectreBar, 10)
				.AddIngredient(ItemID.FallenStar, 20)
				.AddTile(TileID.MythrilAnvil)
				.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            // Loop these functions 3 times.
            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }
    }
}