using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class Genesis_Spear2 : ModProjectile
    {
        public int airTime = 0;
        public int airTimeMax = 15;
        public override void SetDefaults()
        {
            Projectile.width = 132;
            Projectile.height = 132;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.light = .4f;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;

            Projectile.rotation += .4f;

            airTime++;
            if (airTime >= airTimeMax)
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) *30;
            }

            if (Projectile.getRect().Intersects(player.getRect()) && airTime > airTimeMax)
                Projectile.Kill();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;

            var drawPosition = Main.MouseWorld;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            if (Projectile.alpha == 0)
            {
                int direction = -1;

                if (Main.MouseWorld.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }
            return true;
        }
    }
}
