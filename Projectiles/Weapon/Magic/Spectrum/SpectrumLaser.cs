using Microsoft.Xna.Framework;
using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic.Spectrum
{
    public class SpectrumLaser : ModProjectile
    {
        public Color laserColor = Color.Purple;

        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElec();
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = 1;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            player.statMana += 7;
            player.ManaEffect(7);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawProjectilePrims(laserColor, ShardsProjectileHelper.LineX1);
            return base.PreDraw(ref lightColor);
        }
    }
}
