using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic.ByteCrush;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class Bytecrusher : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(5);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.crit = 5;
            Item.mana = 6;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 8f;
            Item.rare = ItemDefaults.RarityMechs;
            Item.value = 120000;
            Item.shoot = ModContent.ProjectileType<ByteBlock>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offset = new(0, 100);

            position = Main.MouseWorld + offset;
            velocity = Vector2.Normalize(Main.MouseWorld - position) * Item.shootSpeed;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            position = Main.MouseWorld - offset;
            velocity = Vector2.Normalize(Main.MouseWorld - position) * Item.shootSpeed;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}