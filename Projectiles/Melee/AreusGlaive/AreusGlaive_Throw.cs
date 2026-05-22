using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusGlaive
{
    public class AreusGlaive_Throw : ModProjectile
    {
        public const int FadeInDuration = 7;
        public const int FadeOutDuration = 4;

        public const int TotalDuration = 40;

        public int Timer;

        public override string Texture => "ShardsOfAtheria/Projectiles/Melee/AreusGlaive/AreusGlaive_Thrust";

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(11);
        }

        public override void SetDefaults()
        {
            Projectile.width = 102;
            Projectile.height = 102;
            Projectile.scale = 1.2f;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
        }
        float speed;

        public override void OnSpawn(IEntitySource source)
        {
            speed = Projectile.velocity.Length();
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.rotation += MathHelper.ToRadians(30f) * Projectile.direction;
            Projectile.SetVisualOffsets(new Vector2(102, 106), true);
            Projectile.spriteDirection = -Projectile.direction;

            Timer++;
            if (Timer >= TotalDuration)
            {
                // Kill the projectile if it reaches it's intented lifetime
                Projectile.Kill();
                return;
            }

            // Fade in and out
            // GetLerpValue returns a value between 0f and 1f - if clamped is true - representing how far Timer got along the "distance" defined by the first two parameters
            // The first call handles the fade in, the second one the fade out.
            // Notice the second call's parameters are swapped, this means the result will be reverted
            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

            // Keep locked onto the player, but extend further based on the given velocity (Requires ShouldUpdatePosition returning false to work)
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Projectile.velocity.Normalize();
            Projectile.velocity *= speed;
            if (Timer % 10 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item71, Projectile.position);
            }
            if (Timer >= 20)
            {
                float reverseTimer = (20 * -1 + (Timer - 20)) * -1;
                Projectile.Center = playerCenter + Projectile.velocity * (reverseTimer + 1f);
            }
            else
            {
                Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
