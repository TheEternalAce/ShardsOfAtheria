using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon
{
    public class JarOIchorProjectile : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 20;
            Projectile.height = 26;

            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.arrow = true;
            AIType = ProjectileID.HolyWater;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Ichor, 20 * 60);
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
