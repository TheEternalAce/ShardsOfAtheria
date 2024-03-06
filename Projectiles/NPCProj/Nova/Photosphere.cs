using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class Photosphere : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            Projectile.ai[0] += 5;
            if (Projectile.ai[0] >= 255)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<ElectricShock>(600);
            if (SoA.Eternity() && Main.rand.NextBool(5))
            {
                target.AddBuff(ModContent.Find<ModBuff>("FargowiltasSouls", "ClippedWingsBuff").Type, 600);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float percent = 1 - Projectile.ai[0] / 255f;
            Projectile.DrawBlurTrail(SoA.HardlightColor * percent, SoA.OrbBlur);
            return base.PreDraw(ref lightColor);
        }
    }
}