using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Projectiles.Weapon.Melee;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class PlumeCodex : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;

            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 2f;
            Item.crit = 6;
            Item.mana = 10;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shootSpeed = 32f;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 1, 75);
            Item.shoot = ModContent.ProjectileType<FeatherBladeFriendly>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit < player.Center.Y + 200f)
            {
                ceilingLimit = player.Center.Y + 200f;
            }
            // Loop these functions 3 times.
            for (int i = 0; i < 3; i++)
            {
                position = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y) + new Vector2(Main.rand.NextFloat(200) * (Main.rand.NextBool(2) ? -1 : 1), 400f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y > 0f)
                {
                    heading.Y *= 1f;
                }

                if (heading.Y > 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }
            return false;
        }
    }
}