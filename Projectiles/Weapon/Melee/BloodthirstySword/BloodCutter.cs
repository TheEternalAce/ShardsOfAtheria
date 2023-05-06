﻿using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.BloodthirstySword
{
    public class BloodCutter : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddIceAqua();
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 240;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }
    }
}
