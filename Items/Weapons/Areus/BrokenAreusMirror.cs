using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Items.Potions;
using Terraria.Audio;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using Terraria.DataStructures;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class BrokenAreusMirror : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MagicMirror;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a broken Areus Mirror that shoots Areus Lasers\n" +
                "Use again to shatter the mirror into 6 shards that deal 1/6 damage each\n" +
                "Only one mirror can be out at a time");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.MagicMirror);

            Item.width = refItem.width;
            Item.height = refItem.height;

            Item.damage = 18;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 0;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;
            Item.noMelee = true;

            Item.shootSpeed = 0;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 10000;
            Item.shoot = ModContent.ProjectileType<AreusMirror>();

            Item.buffType = ModContent.BuffType<AreusMirrorBuff>();
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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AreusMirror>()] == 0)
            {
                Item.buffType = ModContent.BuffType<AreusMirrorBuff>();
            }
            else
            {
                type = ModContent.ProjectileType<AreusMirrorShard>();
                Item.buffType = ModContent.BuffType<AreusMirrorShardBuff>();
            }
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = player.Center + new Vector2(0, -20);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            if (type == ModContent.ProjectileType<AreusMirror>())
            {
                player.ClearBuff(ModContent.BuffType<AreusMirrorShardBuff>());
                // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
                projectile.originalDamage = Item.damage;
            }
            else if (type == ModContent.ProjectileType<AreusMirrorShard>())
            {
                for (int i = 0; i < 6; i++)
                {
                    // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                    var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage/6, knockback, Main.myPlayer);
                    projectile.originalDamage = Item.damage;
                }
            }

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 16)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile(TileID.Hellforge)
                .Register();
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.HasBuff(ModContent.BuffType<Conductive>()))
            {
                damage += .15f;
            }
        }
    }
}
