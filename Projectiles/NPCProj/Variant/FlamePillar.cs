﻿using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class FlamePillar : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(3);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile refProj = new Projectile();
            refProj.SetDefaults(ProjectileID.ClingerStaff);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            for (int num72 = 0; num72 < 5; num72++)
            {
                Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default,
                    2f)];
                obj4.noGravity = true;
                obj4.velocity *= 2f;
                obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                obj4.fadeIn = 1.5f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 60);
        }
    }
}