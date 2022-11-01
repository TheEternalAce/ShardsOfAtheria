using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff
{
    public class IceVortexShard : ModProjectile
	{
        private Vector2 position;
        private double rotation;

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
            Projectile.timeLeft = 600;

            DrawOffsetX = 6;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                position = Projectile.Center;
                Projectile.ai[0] = 1;
            }

            rotation += .5;
            Projectile.rotation = Vector2.Normalize(Projectile.Center - position).ToRotation() + MathHelper.ToRadians(180);
            Projectile.Center = position + Vector2.One.RotatedBy(rotation) * 45;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 600);
        }
    }
}
