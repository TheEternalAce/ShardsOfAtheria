using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Projectiles.Summon.Minions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class BloodTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(1);
            Item.AddElement(3);
            Item.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;

            Item.damage = 95;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 3;
            Item.crit = 18;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item82;
            Item.noMelee = true;

            Item.shootSpeed = 0;
            Item.rare = ItemRarityID.Red;
            Item.value = 321000;
            Item.shoot = ModContent.ProjectileType<BloodSigil>();
            Item.buffType = ModContent.BuffType<BloodySigil>();
        }

        public override bool AltFunctionUse(Player player)
        {
            //return true;
            return base.AltFunctionUse(player);
        }

        public override bool CanUseItem(Player player)
        {
            int sigilCount = player.ownedProjectileCounts[Item.shoot];
            return sigilCount < 6;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            int index = player.ownedProjectileCounts[type] + 1;
            if (index > player.maxMinions + 1)
            {
                index = player.maxMinions + 1;
            }
            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            var projectile = Projectile.NewProjectileDirect(source, position, velocity,
                type, damage, knockback, player.whoAmI, index);
            projectile.originalDamage = Item.damage;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }
    }
}
