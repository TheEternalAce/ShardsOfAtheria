using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.Hunter
{
    public class EntropicArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // The recording mode
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;
            Projectile.arrow = true;

            DrawOffsetX = -8;
        }

        public override void OnSpawn(IEntitySource source)
        {
            //SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);
        }

        public override void AI()
        {
            if (Projectile.alpha > 0) Projectile.alpha -= 25;
            if (Projectile.alpha < 0) Projectile.alpha = 0;
            Entity target = ShardsHelpers.FindClosestNPC(Projectile.Center, null, 500f);
            target ??= ShardsHelpers.FindClosestProjectile(Projectile.Center, 400f, ValidateBomb);
            if (target != null) Projectile.Track(target.Center);
            else Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 16f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public bool ValidateBomb(Projectile projectile)
        {
            if (!projectile.active) return false;
            if (projectile.owner != Projectile.owner) return false;
            if (projectile.type != Type + 1) return false;
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return base.OnTileCollide(oldVelocity);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawAfterImage(lightColor);
            return true;
        }
    }
}
