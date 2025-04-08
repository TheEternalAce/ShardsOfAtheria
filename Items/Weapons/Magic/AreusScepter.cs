using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusScepter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5);
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 68;
            Item.height = 62;

            Item.damage = 54;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3;
            Item.crit = 29;
            Item.mana = 12;

            Item.useTime = 6;
            Item.useAnimation = 24;
            Item.reuseDelay = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item72;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 16;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = 150000;
            Item.shoot = ModContent.ProjectileType<LightningBoltFriendly>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 17)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient<Jade>(6)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddTile<AreusFabricator>()
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

            position -= new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
            position.Y -= 100;
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
            Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, 3f);
            SoundEngine.PlaySound(Item.UseSound, player.Center);
            return false;
        }
    }
}