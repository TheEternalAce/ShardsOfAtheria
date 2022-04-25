﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.NPCProj;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class CrystalExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Explosion");
        }

        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item14);
                Projectile.ai[0] = 1;
            }
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.BlueCrystalShard,
                   Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            dust.velocity += Projectile.velocity * 0.3f;
            dust.velocity *= 0.2f;
            Dust dust1 = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.PinkCrystalShard,
                   Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            dust1.velocity += Projectile.velocity * 0.3f;
            dust1.velocity *= 0.2f;
            Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.PurpleCrystalShard,
                   Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            dust2.velocity += Projectile.velocity * 0.3f;
            dust2.velocity *= 0.2f;
        }
    }
}