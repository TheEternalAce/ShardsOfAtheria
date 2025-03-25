﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ammo
{
    public class TungstenBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.Metalic.Add(Type, 1f);
            Projectile.AddDamageType(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6; // The width of projectile hitbox
            Projectile.height = 6; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.extraUpdates = 2; // Set to above 0 if you want the projectile to update multiple time in a frame

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}
