using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Effects.ScreenShaking;

namespace ShardsOfAtheria.Utilities
{
    public static class ProjectileHelper
    {

        public static void CallStorm(this Projectile projectile, int amount, int pierce = 1)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath56, projectile.Center);
            for (var i = 0; i < amount; i++)
            {
                Projectile p = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(),
                    new Vector2(projectile.Center.X + Main.rand.Next(-60 * amount, 60 * amount), projectile.Center.Y - 600), new Vector2(0, 5),
                    ModContent.ProjectileType<LightningBoltFriendly>(), (int)(projectile.damage * 0.66f), projectile.knockBack, Main.player[projectile.owner].whoAmI);
                p.penetrate = pierce;
                p.DamageType = projectile.DamageType;
            }
        }

        public static void Explode(this Projectile proj, Vector2 position, int explosionSize = 120)
        {
            Projectile explosion = Projectile.NewProjectileDirect(proj.GetSource_FromThis(), position, Vector2.Zero,
                ModContent.ProjectileType<ElementExplosion>(), proj.damage, proj.knockBack, proj.owner);
            explosion.DamageType = proj.DamageType;
            explosion.Size = new Vector2(explosionSize);
            ProjectileElements elementExplosion = explosion.GetGlobalProjectile<ProjectileElements>();
            SoAGlobalProjectile globalExplosion = explosion.GetGlobalProjectile<SoAGlobalProjectile>();
            int type = proj.type;
            if (ProjectileElements.Fire.Contains(type))
            {
                elementExplosion.tempFire = true;
            }
            if (ProjectileElements.Ice.Contains(type))
            {
                elementExplosion.tempIce = true;
            }
            if (ProjectileElements.Electric.Contains(type))
            {
                elementExplosion.tempElectric = true;
            }
            if (ProjectileElements.Metal.Contains(type))
            {
                elementExplosion.tempMetal = true;
            }
            if (SoAGlobalProjectile.AreusProj.Contains(type))
            {
                globalExplosion.tempAreus = true;
            }
        }

        public static void Explode(this Projectile proj, int explosionSize = 120)
        {
            Vector2 newExplosionSize = new Vector2(explosionSize);
            SoAGlobalProjectile globalExplosion = proj.GetGlobalProjectile<SoAGlobalProjectile>();
            globalExplosion.explosion = true;
            proj.timeLeft = 10;
            proj.velocity *= 0f;
            proj.position += (proj.Size - newExplosionSize) / 2;
            proj.Size = newExplosionSize;
            ScreenShake.ShakeScreen(6, 60);
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 10; i++)
            {
                if (ProjectileElements.Fire.Contains(proj.type))
                {
                    Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Torch, Scale: 1.3f);
                    dust.velocity *= 4f;
                }
                if (ProjectileElements.Ice.Contains(proj.type))
                {
                    Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Ice);
                    dust.velocity *= 4f;
                }
                if (ProjectileElements.Electric.Contains(proj.type))
                {
                    Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Electric);
                    dust.velocity *= 4f;
                }
                if (ProjectileElements.Metal.Contains(proj.type))
                {
                    Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Iron, Scale: 1.3f);
                    dust.velocity *= 4f;
                }
                Dust dust2 = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Smoke, Scale: 1.5f);
                dust2.velocity *= 2f;
            }
        }

        public static void TrackTarget(this Projectile projectile, NPC targetNPC, int speed = 16, int inertia = 16)
        {
            if (Vector2.Distance(projectile.Center, targetNPC.Center) > 40f)
            {
                // The immediate range around the target (so it doesn't latch onto it when close)
                Vector2 direction = targetNPC.Center - projectile.Center;
                direction.Normalize();
                direction *= speed;

                projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
            }
        }

        public static SoAGlobalProjectile ShardsOfAtheria(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<SoAGlobalProjectile>();
        }

        public static OverchargedProjectile Overcharged(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<OverchargedProjectile>();
        }
    }
}
