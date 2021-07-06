using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class PrometheusSword : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.arrow = false;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;

            drawOffsetX = -4;
            drawOriginOffsetX = 2;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.Fire,
                    projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.Fire,
                    0, 0, 254, Scale: 0.3f);
                dust.velocity += projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            // This code spawns 3 projectiles in the opposite direction of the projectile, with random variance in velocity.
            if (projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Calculate new speeds for other projectiles.
                    // Rebound at 40% to 70% speed, plus a random amount between -8 and 8
                    float speedX = -projectile.velocity.X * Main.rand.NextFloat(.4f, .7f) + Main.rand.NextFloat(-8f, 8f);
                    float speedY = -projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f; // This is Vanilla code, a little more obscure.
                                                                                                                             // Spawn the Projectile.
                    Projectile.NewProjectile(projectile.position.X + speedX, projectile.position.Y + speedY, speedX, speedY, ModContent.ProjectileType<PrometheusFireGrav>(), (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
                }
            }
            target.AddBuff(BuffID.CursedInferno, 10 * 60);
            target.AddBuff(BuffID.Ichor, 10 * 60);
            Main.PlaySound(SoundID.Item74, projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // This code spawns 3 projectiles in the opposite direction of the projectile, with random variance in velocity.
            if (projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Calculate new speeds for other projectiles.
                    // Rebound at 40% to 70% speed, plus a random amount between -8 and 8
                    float speedX = -projectile.velocity.X * Main.rand.NextFloat(.4f, .7f) + Main.rand.NextFloat(-8f, 8f);
                    float speedY = -projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f; // This is Vanilla code, a little more obscure.
                                                                                                                             // Spawn the Projectile.
                    Projectile.NewProjectile(projectile.position.X + speedX, projectile.position.Y + speedY, speedX, speedY, ModContent.ProjectileType<PrometheusFireGrav>(), (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
                }
                Main.PlaySound(SoundID.Item74, projectile.position);
            }
            return base.OnTileCollide(oldVelocity);
        }
    }
}
