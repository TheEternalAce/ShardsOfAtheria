using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic.Gambit;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusGambit : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus(true, true);
            Item.AddDamageType(1, 5);
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;

            Item.damage = 50;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 2;
            Item.crit = 2;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoA.Coin;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 10;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = 50000;
            Item.shoot = ModContent.ProjectileType<ElecCoin>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 2)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 15)
                .AddTile<AreusFabricator>()
                .AddCondition(Condition.NearLava)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 2)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 15)
                .AddTile<AreusFabricator>()
                .AddTile(TileID.Hellforge)
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
                Item.UseSound = SoA.Coin;
                Item.useTime = Item.useAnimation = 20;
                Item.shoot = ModContent.ProjectileType<ElecCoin>();
            }
            if (Item.shoot == ModContent.ProjectileType<ElecCoin>())
            {
                return player.ownedProjectileCounts[Item.shoot] < 4;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                if (player.Shards().Overdrive)
                {
                    type = ModContent.ProjectileType<ElecScorpionFang>();
                }
            }
            if (type == ModContent.ProjectileType<ElecCoin>())
            {
                if (player.controlUp)
                {
                    velocity = velocity.RotatedBy(MathHelper.ToRadians(-15) * player.direction);
                    velocity.Normalize();
                    velocity = velocity * 10;
                }
                else velocity = new Vector2(0, -1) * 10;
                velocity += player.velocity;
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public void SetAttack(Player player)
        {
            Item.UseSound = SoundID.Item43;
            Item.mana = 12;
            string text = "Tails";
            bool heads = Main.rand.NextBool(2);
            int riggedCoin = player.Shards().riggedCoin;
            if (riggedCoin == 2)
            {
                heads = true;
            }
            else if (riggedCoin == 1)
            {
                heads = false;
            }

            if (heads)
            {
                Item.shoot = ModContent.ProjectileType<ElecScorpionFang>();
                Item.shootSpeed = 1;
                Item.damage = 75;
                Item.useTime = Item.useAnimation = 30;
                text = "Heads";
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<ElecScorpionTail>();
                Item.shootSpeed = 10;
                Item.damage = 50;
            }
            player.AddBuff<SpareChange>(600);
            CombatText.NewText(player.Hitbox, Color.Cyan, text);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<ElecScorpionFang>())
            {
                if (player.Shards().Overdrive)
                {
                    Item.UseSound = SoundID.Item43;
                    if (Item.shootSpeed != 10)
                    {
                        velocity.Normalize();
                        velocity *= 10f;
                    }
                    Projectile.NewProjectile(source, position, velocity,
                        ModContent.ProjectileType<ElecScorpionTail>(), damage, knockback,
                        player.whoAmI);
                }
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(15);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(
                        -rotation, rotation, i / (numberProjectiles - 1)));
                    perturbedSpeed.Normalize();
                    perturbedSpeed *= 12f;
                    Projectile.NewProjectile(source, position, perturbedSpeed, type,
                        damage, knockback, player.whoAmI, 0, 1 - 2 * i);
                }
                return false;
            }
            if (type == ModContent.ProjectileType<ElecCoin>() && player.controlUp)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}