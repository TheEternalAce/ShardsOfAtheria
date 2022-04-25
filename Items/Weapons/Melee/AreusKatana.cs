using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Weapon.Melee;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusKatana : AreusWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Holding this weapon increases your movement speed by 5%\n" +
                "'Call me Karlson'");
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 42;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0,  6);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<ElectricKunai>();
            Item.shootSpeed = 6;

            if (ModContent.GetInstance<ServerSideConfig>().areusWeaponsCostMana)
                Item.mana = 9;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 17)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddTile(ModContent.TileType<AreusForge>())
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(10);
            position += Vector2.Normalize(velocity) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
        }
    }
}