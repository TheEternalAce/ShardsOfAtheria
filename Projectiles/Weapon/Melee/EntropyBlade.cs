using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using MMZeroElements;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class EntropyBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            ProjectileElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;

            DrawOffsetX = -32;
            DrawOriginOffsetY = -22;
            DrawOriginOffsetX = -13;
        }

        public bool Cooldown = false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 0f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.spriteDirection == -1)
            {
                num = 3.14159274f;
            }
            if (++Projectile.frameCounter == 4)
            {
                if (++Projectile.frame > Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                }
                Projectile.frameCounter = 0;
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -32;
                DrawOriginOffsetY = -22;
                DrawOriginOffsetX = -13;
            }
            else
            {
                DrawOffsetX = -58;
                DrawOriginOffsetY = -22;
                DrawOriginOffsetX = 13;
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint4 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.position, Projectile.Center + Projectile.velocity + Projectile.velocity.SafeNormalize(Vector2.Zero) * 40f, 56f * Projectile.scale, ref collisionPoint4))
            {
                return true;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<LoomingEntropy>(), 600);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<EntropySlash>(), 300, 0f, Projectile.owner);
        }
    }
}