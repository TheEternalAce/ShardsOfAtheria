using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
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
            Item.AddDamageType(11, 5);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;

            Item.damage = 67;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.shootSpeed = 16;
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = Item.sellPrice(0, 3, 50);
            Item.shoot = ModContent.ProjectileType<ValkyrieStormSword>();
        }

        public override bool MeleePrefix()
        {
            return true;
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
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<StormSlash>(), damage, knockback, player.whoAmI,
                player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            for (int i = 0; i < 3; i++)
            {
                position -= new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100;
                Vector2 heading = target - position;

                if (heading.Y < 0f) heading.Y *= -1f;

                if (heading.Y < 20f) heading.Y = 20f;

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                heading = heading.RotatedByRandom(MathHelper.ToRadians(1f));
                Projectile.NewProjectile(source, position, heading, ModContent.ProjectileType<StormBlade>(), damage * 2, knockback, player.whoAmI, 0f, 3f);
            }

            if (player.ownedProjectileCounts[type] > 0) return false;
            else return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}