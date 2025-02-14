﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.GenesisRagnarok.IceStuff
{
    public class IceVortexShard : ModProjectile
    {
        private Vector2 position;
        private double rotation;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(4);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 120;

            DrawOffsetX = 6;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                position = Projectile.Center;
                Projectile.ai[0] = 1;
            }

            rotation += .2;
            Projectile.rotation = Vector2.Normalize(Projectile.Center - position).ToRotation() + MathHelper.Pi;
            Projectile.Center = position + Vector2.One.RotatedBy(rotation) * 45;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 600);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Ice);
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
        }
    }
}
