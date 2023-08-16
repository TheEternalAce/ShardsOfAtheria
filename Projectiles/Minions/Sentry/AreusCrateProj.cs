using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions.Sentry
{
    public class AreusCrateProj : ModProjectile
    {
        int gravityTimer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 34; // The width of projectile hitbox
            Projectile.height = 22; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.DamageType = DamageClass.Summon; // Is the projectile shoot by a ranged weapon?
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(32) * Projectile.direction;
            gravityTimer++;
            if (gravityTimer >= 16)
            {
                if (++Projectile.velocity.Y > 16)
                {
                    Projectile.velocity.Y = 16;
                }
                gravityTimer = 16;
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override void Kill(int timeLeft)
        {
            var player = Main.player[Projectile.owner];
            int currentTurretIndex = 0; // The javelin index
            Point[] turrets = new Point[player.maxTurrets];

            var position = Projectile.Center + new Vector2(0, -10);
            int newTurretIndex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position,
                Vector2.Zero, ModContent.ProjectileType<AreusTurret>(), Projectile.damage, Projectile.knockBack,
                Projectile.owner);

            for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != newTurretIndex // Make sure the looped projectile is not the current javelin
                    && currentProjectile.active // Make sure the projectile is active
                    && currentProjectile.owner == Main.myPlayer // Make sure the projectile's owner is the client's player
                    && currentProjectile.sentry) // Make sure the projectile is a sentry
                {
                    turrets[currentTurretIndex++] = new Point(i, currentProjectile.timeLeft); // Add the current projectile's index and timeleft to the point array
                    if (currentTurretIndex >= turrets.Length)  // If the javelin's index is bigger than or equal to the point array's length, break
                        break;
                }
            }

            // Remove the oldest sticky javelin if we exceeded the maximum
            if (currentTurretIndex >= player.maxMinions)
            {
                int oldTurretIndex = 0;
                // Loop our point array
                for (int i = 0; i < player.maxMinions; i++)
                {
                    // Remove the already existing javelin if it's timeLeft value (which is the Y value in our point array) is smaller than the new javelin's timeLeft
                    if (turrets[i].Y < turrets[oldTurretIndex].Y)
                    {
                        oldTurretIndex = i; // Remember the index of the removed javelin
                    }
                }
                // Remember that the X value in our point array was equal to the index of that javelin, so it's used here to kill it.
                Main.projectile[turrets[oldTurretIndex].X].Kill();
            }

            SoundEngine.PlaySound(SoundID.Item53, Projectile.Center);
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }
    }
}
