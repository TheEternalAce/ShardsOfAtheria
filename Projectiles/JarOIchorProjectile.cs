using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class JarOIchorProjectile : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 20;
            projectile.height = 26;

            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            aiType = ProjectileID.HolyWater;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Ichor, 20 * 60);
            Main.PlaySound(SoundID.Shatter, projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Shatter, projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
