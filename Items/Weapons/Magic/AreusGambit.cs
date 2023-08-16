using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Magic.Gambit;
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
            Item.ResearchUnlockCount = 1;
            Item.AddAreus(true);
            SoAGlobalItem.UpgradeableItem.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;

            Item.damage = 50;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 2;
            Item.crit = 2;
            Item.mana = 12;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoA.Coin;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 10;
            Item.rare = ItemRarityID.Cyan;
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

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.Shards().Overdrive)
            {
                type = ModContent.ProjectileType<LightningBoltFriendly>();
            }
            if (type == ModContent.ProjectileType<ElecCoin>())
            {
                velocity = new Vector2(0, -1) * 10 + player.velocity;
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public void SetAttack(Player player)
        {
            Item.UseSound = SoundID.Item43;
            string text = "Tails";
            if (Main.rand.NextBool(2))
            {
                Item.shoot = ModContent.ProjectileType<LightningBoltFriendly>();
                Item.shootSpeed = 1;
                text = "Heads";
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<ElecScorpionTail>();
                Item.shootSpeed = 16;
            }
            CombatText.NewText(player.Hitbox, Color.Cyan, text);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Shards().Overdrive)
            {
                Item.UseSound = SoundID.Item43;
                Projectile.NewProjectile(source, position, velocity * 16f,
                    ModContent.ProjectileType<ElecScorpionTail>(), damage, knockback, player.whoAmI);
                type = ModContent.ProjectileType<LightningBoltFriendly>();
            }
            else if (Item.shoot != ModContent.ProjectileType<ElecCoin>() && !player.Shards().Overdrive)
            {
                SetDefaults();
            }
            if (type == ModContent.ProjectileType<LightningBoltFriendly>())
            {
                float numberProjectiles = 3; // 3 shots
                float rotation = MathHelper.ToRadians(10);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    perturbedSpeed.Normalize();
                    Projectile.NewProjectile(source, position, perturbedSpeed * 10f, type, damage, knockback, player.whoAmI);
                }
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}