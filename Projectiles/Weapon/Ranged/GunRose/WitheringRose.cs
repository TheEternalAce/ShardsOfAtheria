﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.GunRose
{
    public class WitheringRose : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile refProj = new();
            refProj.SetDefaults(ProjectileID.FlowerPow);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.scale = 0;
        }

        public override void AI()
        {
            if (Projectile.scale < 1f)
            {
                Projectile.scale += 0.1f;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 60)
            {
                ShardsHelpers.ProjectileRing(Projectile.GetSource_FromThis(), Projectile.Center,
                    5, 1, 16f, ModContent.ProjectileType<WitheringPetal>(), Projectile.damage,
                    Projectile.knockBack, Projectile.owner);
                Projectile.ai[0] = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture).Value,
                Projectile.position - Main.screenPosition - new Vector2(17) * Projectile.scale,
                null, lightColor, 0f, Vector2.Zero, Projectile.scale, SpriteEffects.None,
                1);
            return false;
        }
    }
}