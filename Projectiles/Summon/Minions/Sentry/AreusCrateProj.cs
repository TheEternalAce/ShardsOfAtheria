using Microsoft.Xna.Framework;
using ShardsOfAtheria.Gores;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Sentry
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
            Projectile.rotation += MathHelper.ToRadians(30) * Projectile.direction;
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

        public override void OnKill(int timeLeft)
        {
            var player = Main.player[Projectile.owner];

            var position = Projectile.Center + new Vector2(0, -10);
            int newTurretIndex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position,
                Vector2.Zero, ModContent.ProjectileType<AreusTurret>(), Projectile.damage, Projectile.knockBack,
                Projectile.owner);

            ShardsHelpers.KillOldestSentry(player, newTurretIndex);

            SoundEngine.PlaySound(SoundID.Item53, Projectile.Center);
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
                if (i < 1)
                {
                    Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center,
                        speed, ShardsGores.AreusCratePart.Type);
                    Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center,
                        speed, ShardsGores.AreusCratePart2.Type);
                }
            }
        }
    }
}
