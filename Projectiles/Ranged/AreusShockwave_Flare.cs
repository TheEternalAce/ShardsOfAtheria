using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusShockwave_Flare : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 5;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

        public override void AI()
        {
            Projectile.scale++;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = (int)(26 * Projectile.scale);
            int halfSize = size / 2;
            hitbox.Width = hitbox.Height = size;
            hitbox.X = (int)(Projectile.Center.X - halfSize);
            hitbox.Y = (int)(Projectile.Center.Y - halfSize);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.ElectricColor;
            Projectile.DrawBlurTrail(lightColor, ShardsHelpers.Orb, scale: Projectile.scale);
            return false;
        }
    }
}
