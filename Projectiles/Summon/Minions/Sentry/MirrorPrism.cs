﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Sentry
{
    public class MirrorPrism : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.SentryShot[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 34;
            Projectile.aiStyle = -1;

            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
            }
        }

        public override void AI()
        {
            if (++Projectile.ai[0] >= 30)
            {
                Projectile.velocity *= 0.8f;
            }
            RepellPrisms(50);
        }

        private void RepellPrisms(float maxDistance)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active && projectile.whoAmI != Projectile.whoAmI && projectile.type == Type)
                {
                    var distToPlayer = Vector2.Distance(projectile.Center, Projectile.Center);
                    if (distToPlayer <= maxDistance)
                    {
                        var vector = projectile.Center - Projectile.Center;
                        vector.Normalize();
                        Projectile.velocity = vector * -4;
                        projectile.velocity = vector * 4;
                    }
                }
            }
        }

        public static int FindNewestProjectile(int owner, int mirrorIndex)
        {
            int result = -1;
            int timeLeft = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.type == ModContent.ProjectileType<MirrorPrism>() && projectile.owner == owner && projectile.ai[1] == mirrorIndex)
                {
                    if (projectile.timeLeft > timeLeft)
                    {
                        result = projectile.whoAmI;
                        timeLeft = projectile.timeLeft;
                    }
                }
            }
            return result;
        }

        public static int CountPrismsPerMirror(int owner, int mirrorIndex)
        {
            int result = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.type == ModContent.ProjectileType<MirrorPrism>() && projectile.owner == owner && projectile.ai[1] == mirrorIndex &&
                    Math.Abs(projectile.velocity.X) < 1 && Math.Abs(projectile.velocity.Y) < 1)
                {
                    result++;
                }
            }
            return result;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
