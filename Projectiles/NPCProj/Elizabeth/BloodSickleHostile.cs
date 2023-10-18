using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodSickleHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.timeLeft = 60 * 5;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        Vector2 InitialVelocity = Vector2.Zero;
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }

        public override void AI()
        {
            if (Projectile.frame < 3)
            {
                if (++Projectile.frameCounter >= 10)
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                }
            }
            else if (Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = InitialVelocity;
            }
            else
            {
                if (Projectile.ai[0] == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
                    Projectile.ai[0]++;
                }
                Projectile.rotation += MathHelper.ToRadians(15f);
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.type != ModContent.NPCType<Death>())
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<DeathBleed>(300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            //Projectile.DrawPrimsAfterImage(lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}