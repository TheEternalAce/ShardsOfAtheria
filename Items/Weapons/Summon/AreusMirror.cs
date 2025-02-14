using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Summon.Minions.Sentry;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class AreusMirror : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;

            Item.damage = 62;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 5;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;
            Item.noMelee = true;

            Item.shootSpeed = 0;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
            Item.shoot = ModContent.ProjectileType<AreusMirrorSentry>();
            Item.sentry = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer, 1);
            projectile.originalDamage = Item.damage;
            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            ShardsHelpers.KillOldestSentry(player, projectile.whoAmI);
            return false;
        }
    }
}
