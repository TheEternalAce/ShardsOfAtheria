using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusKatana : AreusWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Call me Carlson");
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
            Item.value = Item.sellPrice(gold: 6);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 21;
            Item.shoot = ModContent.ProjectileType<ElectricKunai>();
            Item.shootSpeed = 10;

            if (!ModContent.GetInstance<Config>().areusWeaponsCostMana)
                areusResourceCost = 3;
            else Item.mana = 9;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusBarItem>(), 17)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddTile(ModContent.TileType<AreusForge>())
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(45);
            position += Vector2.Normalize(velocity) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
        }
    }
}