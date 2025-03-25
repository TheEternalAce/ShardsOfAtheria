﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class VoidDagger : ModProjectile
    {
        int TargetWhoAmI => (int)Projectile.ai[1];

        float Timer
        {
            get => Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            Projectile.AddDamageType(6);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;

            Projectile.aiStyle = 0;
            Projectile.tileCollide = false;
            Projectile.light = 1;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Magic;

            DrawOffsetX = -5;
        }

        public override void AI()
        {
            if (TargetWhoAmI >= 0)
            {
                var target = Main.npc[TargetWhoAmI];
                if (!target.CanBeChasedBy())
                {
                    Projectile.Kill();
                }
                var vector = target.Center - Projectile.Center;
                vector.Normalize();
                Projectile.rotation = vector.ToRotation() + MathHelper.ToRadians(90f);

                float rotation = MathHelper.Pi / 3;
                Projectile.Center = target.Center + Vector2.One.RotatedBy(rotation * Projectile.ai[0]) * 90;
                const float TimeBeforeFlight = 15f;
                if (++Timer > TimeBeforeFlight)
                {
                    float travel = (Timer - TimeBeforeFlight) * 8;
                    Projectile.Center += vector * travel;
                }
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Projectile.GetPlayerOwner();
            if (!player.HasBuff<ShadeState>())
            {
                var areus = player.Areus();
                areus.imperialVoid -= 3;
            }
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawBloomTrail(lightColor.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2);
            Projectile.DrawAfterImage(lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}
