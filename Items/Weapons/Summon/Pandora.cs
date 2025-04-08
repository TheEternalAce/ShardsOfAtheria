using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Summon.Active;
using ShardsOfAtheria.Projectiles.Summon.Minions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class Pandora : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(2, 5);
            Item.AddElement(1);
            Item.AddElement(2);
            Item.AddRedemptionElement(4);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.damage = 77;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 0f;
            Item.mana = 6;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.staff[Item.type] = true;

            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(0, 3, 25);
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.mana = 0;
                Item.knockBack = 0f;
                Item.shoot = ModContent.ProjectileType<FrostsparkDrone>();
                Item.buffType = ModContent.BuffType<FrostsparkDroneBuff>();
                Item.UseSound = SoundID.Item43;
            }
            else
            {
                Item.mana = 8;
                Item.knockBack = 3f;
                Item.shoot = ModContent.ProjectileType<FrostsparkBeam>();
                Item.buffType = 0;
                Item.UseSound = null;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2) position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.ownedProjectileCounts[type] > 0)
                {
                    player.Shards().frostsparkDronesTier++;
                    return false;
                }
                else player.Shards().frostsparkDronesTier = 0;
                // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
                player.AddBuff(Item.buffType, 2);

                for (int i = 0; i < 2; i++)
                {
                    // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                    var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, i);
                    projectile.originalDamage = Item.damage;
                }
                player.ownedProjectileCounts[type] = 2;
                // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}