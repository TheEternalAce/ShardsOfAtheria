﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Spectrum
{
    public class SpectrumLaser : ModProjectile
    {
        public Color laserColor = Color.Purple;

        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Projectile.AddDamageType(4);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
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
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Projectile.GetPlayerOwner();
            int manaGain = (int)(7 * player.manaCost);
            player.statMana += manaGain;
            player.ManaEffect(manaGain);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBloomTrail(laserColor.UseA(50), SoA.LineBloom);
            return base.PreDraw(ref lightColor);
        }
    }
}
