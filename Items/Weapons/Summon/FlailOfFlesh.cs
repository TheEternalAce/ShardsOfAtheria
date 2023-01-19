using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class FlailOfFlesh : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 38;

            Item.damage = 50;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 4f;

            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 20f;
            Item.value = Item.sellPrice(0, 2, 25);
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<FlailOfFleshProj>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(0, 3); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(20);
            position += Vector2.Normalize(velocity) * 20f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 Projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
        }
    }
}