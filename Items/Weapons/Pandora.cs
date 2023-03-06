using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    public class Pandora : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Ice.Add(Type);
            WeaponElements.Electric.Add(Type);
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.damage = 107;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.mana = 6;

            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.staff[Item.type] = true;

            Item.shootSpeed = 15f;
            Item.value = Item.sellPrice(0, 3, 25);
            Item.rare = ItemRarityID.Red;
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
                Item.useTime = 16;
                Item.useAnimation = 16;
                Item.UseSound = SoundID.Item28;
                Item.damage = 87;
                Item.DamageType = DamageClass.Magic;
                Item.mana = 8;
                Item.knockBack = 3;
                Item.shoot = ModContent.ProjectileType<IceBolt>();
                Item.shootSpeed = 16f;
                Item.buffType = 0;
            }
            else
            {
                Item.useTime = 35;
                Item.useAnimation = 35;
                Item.UseSound = SoundID.Item43;
                Item.damage = 107;
                Item.DamageType = DamageClass.Summon;
                Item.mana = 0;
                Item.knockBack = 0;
                Item.shoot = ModContent.ProjectileType<FrostsparkDrone>();
                Item.shootSpeed = 1f;
                Item.buffType = ModContent.BuffType<FrostsparkDroneBuff>();
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                position = Main.MouseWorld;
            }
            else
            {
                base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            }
        }

        bool nextElectric = false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
                player.AddBuff(Item.buffType, 2);

                for (int i = 0; i < 2; i++)
                {
                    // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                    var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
                    projectile.originalDamage = Item.damage;
                    (projectile.ModProjectile as FrostsparkDrone).electricMode = nextElectric;
                    nextElectric = !nextElectric;
                }
                // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}