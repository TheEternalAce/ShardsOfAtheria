using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class ValkyrieStormSword : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/ValkyrieStorm";

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ModContent.ItemType<ValkyrieStorm>());

            Projectile.width = refItem.width;
            Projectile.height = refItem.height;

            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!CheckActive(player))
            {
                return;
            }
            Projectile.rotation += MathHelper.ToRadians(30);
            if (Main.myPlayer == Projectile.owner)
            {
                if (Main.mouseLeft)
                {
                    float speed = 25f;
                    Projectile.Track(Main.MouseWorld, -1, speed, speed / 2);
                }
                else
                {
                    Projectile.Track(player.Center, -1);
                    if (Projectile.Hitbox.Intersects(player.getRect()))
                    {
                        Projectile.Kill();
                    }
                }
            }
        }

        bool CheckActive(Player player)
        {
            if (player == null)
            {
                return false;
            }
            if (player.dead || !player.active)
            {
                return false;
            }
            Projectile.timeLeft = 2;
            return true;
        }
    }
}
