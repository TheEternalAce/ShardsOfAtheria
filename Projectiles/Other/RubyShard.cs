using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class RubyShard : ModProjectile
    {
        int gravityTimer;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 12;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = 0;
            gravityTimer = 16;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ApplyGravity(ref gravityTimer);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            var vector = -Projectile.velocity;
            vector.Normalize();
            vector *= 3f;
            for (int i = 0; i < 6; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby,
                    vector.X, vector.Y);
                dust.noGravity = true;
            }
        }
    }

    internal class RubyShardNPC : GlobalNPC
    {
        int hitCooldown = 0;

        public override bool InstancePerEntity => true;

        public override void PostAI(NPC npc)
        {
            if (hitCooldown > 0)
            {
                hitCooldown--;
            }
            base.PostAI(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ModContent.ProjectileType<RubyShard>())
            {
                hitCooldown = 30;
            }
            base.OnHitByProjectile(npc, projectile, hit, damageDone);
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (projectile.type == ModContent.ProjectileType<RubyShard>())
            {
                if (hitCooldown > 0)
                {
                    return false;
                }
            }
            return base.CanBeHitByProjectile(npc, projectile);
        }
    }
}
