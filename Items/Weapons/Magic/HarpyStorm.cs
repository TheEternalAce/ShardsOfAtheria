using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using SagesMania.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Magic
{
    public class HarpyStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Rains feather blades on your enemies'");
        }

        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 67;
            item.magic = true;
            item.crit = 16;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<FeatherBladeFriendly>();
            item.shootSpeed = 8;
        }

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ValkyrieBlade>());
			recipe.AddIngredient(ItemID.SpectreBar, 10);
			recipe.AddIngredient(ItemID.FallenStar, 20);
			recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				position.Y -= (100 * i);
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
				heading *= new Vector2(speedX, speedY).Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, 0f, ceilingLimit);
			}
			return false;
		}
	}
}