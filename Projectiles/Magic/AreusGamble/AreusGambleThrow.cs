﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.AreusGamble
{
    public class AreusGambleThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(5);

            SoAGlobalProjectile.Metalic.Add(Type, 0.2f);
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.Pi / 6 * Projectile.direction;
            float maxDetectDistance = 1000f;
            var target = Projectile.FindClosestNPC(null, maxDetectDistance);
            if (target != null)
            {
                Projectile.Track(target);
            }
        }
    }
}
