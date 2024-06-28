using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.PlagueRail
{
    public class PlagueBullet : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_927";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 5;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            //Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            int dustDelay = 11;
            if (++Projectile.ai[0] >= dustDelay)
            {
                if (!Projectile.hostile)
                {
                    Projectile.hostile = true;
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff<PlagueMark>())
            {
                modifiers.Defense -= 0.1f;
                modifiers.ScalingBonusDamage += 0.1f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.HasBuff<Plague>()) target.AddBuff<Plague>(300);
            target.AddBuff<PlagueMark>(600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.PaleGreen.UseA(50);
            Projectile.DrawBloomTrail(lightColor, SoA.LineBloom);
            //Projectile.DrawAfterImage(lightColor);
            return false;
        }
    }
}
