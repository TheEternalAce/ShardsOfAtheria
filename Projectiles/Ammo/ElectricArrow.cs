using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ammo
{
    public class ElectricArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(5);
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

            DrawOffsetX = 2;
        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, Vector2.Zero);
                dust.velocity = Projectile.velocity / i / 2;
                dust.noGravity = true;
            }
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Projectile.timeLeft == 30)
            {
                Projectile.velocity *= -1;
                Projectile.penetrate = 1;
                Projectile.netUpdate = true;
            }
            if (Projectile.timeLeft < 30)
            {
                float maxDetectRadius = 400f;

                NPC closestNPC = Projectile.FindClosestNPC(null, maxDetectRadius);
                if (closestNPC == null)
                    return;

                Projectile.Track(closestNPC, inertia: 8f);
                Projectile.netUpdate = true;
                Projectile.timeLeft++;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.ElectricColorA0;
            return base.PreDraw(ref lightColor);
        }
    }
}
