﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar
{
    public class PsychicBlade : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5, 9);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(13);
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 5;
        }

        public override void AI()
        {
            var owner = Projectile.GetPlayerOwner();
            var target = Projectile.FindClosestNPC(null, 500);
            if (owner.HasMinionAttackTargetNPC) target = Main.npc[owner.MinionAttackTargetNPC];
            if (target != null) Projectile.Track(target, 10, 10);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.HardlightColor;
            Projectile.DrawBloomTrail(lightColor.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2, 0.6f);
            return base.PreDraw(ref lightColor);
        }
    }
}
