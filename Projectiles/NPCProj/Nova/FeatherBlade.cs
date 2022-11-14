﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using ShardsOfAtheria.Buffs.AnyDebuff;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class FeatherBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.damage = 37;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;

            DrawOffsetX = -4;
            DrawOriginOffsetX = 2;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Projectile.ai[0] == 0 && Projectile.ai[1] > 0)
            {
                Projectile.timeLeft = Convert.ToInt32(Projectile.ai[1]);
                Projectile.ai[0] = 1;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 300);
            }
        }
    }
}