﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Melee.ElecKatana
{
    public class ElecKunaiHoming : ElecKunai
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/Melee/ElecKatana/ElecKunai";

        NPC target;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            if (target == null)
            {
                target = Projectile.FindClosestNPC(null, 1000);
                return;
            }
            Projectile.Track(target);

            if (!target.active || target.life <= 0)
            {
                Projectile.Kill();
            }

            if (Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, 0, 0, 200, Scale: 1f);
                dust.noGravity = true;
            }
        }
    }
}
