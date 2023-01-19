using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon.Minion
{
    public class NestlingStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;

            Item.damage = 20;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 0;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item82;
            Item.noMelee = true;

            Item.shootSpeed = 0;
            Item.rare = ItemRarityID.Green;
            Item.value = 17500;
            Item.shoot = ModContent.ProjectileType<YoungHarpy>();

            Item.buffType = ModContent.BuffType<JuvenileHarpy>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
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

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }
    }
}
