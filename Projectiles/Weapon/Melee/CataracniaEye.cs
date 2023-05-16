using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class CataracniaEye : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAqua();
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.aiStyle = -1;
        }

        int gravity;
        public override void AI()
        {
            if (++gravity >= 16)
            {
                if (++Projectile.velocity.Y > 16)
                {
                    Projectile.velocity.Y = 16;
                }
            }
            ModContent.GetInstance<CameraFocus>().SetTarget("CataracniaEye", Projectile.Center, CameraPriority.Weak);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (oldVelocity.Y > 6)
            {
                SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.position);
            }

            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            Projectile.velocity.X *= 0.8f;
            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.velocity.Y *= 0.8f;
            if (oldVelocity.Y < 6)
            {
                Projectile.velocity.Y = 0;
            }
            return false;
        }
    }
}
