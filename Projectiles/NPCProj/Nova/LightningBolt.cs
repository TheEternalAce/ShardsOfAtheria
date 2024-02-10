using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class LightningBolt : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 140;
        }

        Vector2 initialVel = Vector2.Zero;
        int initialDmg = 0;
        int DustTimer = 0;

        public override void AI()
        {
            if (initialVel == Vector2.Zero)
            {
                initialVel = Projectile.velocity;
                initialDmg = Projectile.damage;
            }
            if (++Projectile.ai[0] > 4)
            {
                Projectile.velocity = initialVel.RotatedByRandom(MathHelper.ToRadians(35));
                Projectile.ai[0] = 0;
            }

            DustTimer++;
            if (DustTimer > 17 || Projectile.ai[1] == 2)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
        }
    }
}
