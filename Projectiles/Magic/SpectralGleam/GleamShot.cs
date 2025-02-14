using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.SpectralGleam
{
    public class GleamShot : ModProjectile
    {
        Vector2 initialVelocity = Vector2.Zero;
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddRedemptionElement(5);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 353;
            Projectile.extraUpdates = 20;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item84, Projectile.Center);
            SoundEngine.PlaySound(SoundID.NPCDeath7, Projectile.Center);
            initialVelocity = Projectile.velocity;
            Projectile.velocity *= 0f;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            int dustType = DustID.GemDiamond;
            int shootTime = 15 * (Projectile.extraUpdates + 1);
            if (Projectile.ai[0] < shootTime && Projectile.ai[0] % 40 == 0)
            {
                for (int i = 0; i++ < 4;)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center, dustType);
                    d.velocity = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 5f;
                    //d.fadeIn = 1.3f;
                    d.noGravity = true;
                }
            }
            else if (Projectile.ai[0] == shootTime)
            {
                ShardsHelpers.DustRing(Projectile.Center, 2f, dustType, 28, 0f);
                SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
                Projectile.velocity = initialVelocity;
                Projectile.friendly = true;
            }
            else if (Projectile.ai[0] >= shootTime)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, dustType);
                d.velocity *= 0;
                //d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X = (int)Projectile.Center.X - 15;
            hitbox.Y = (int)Projectile.Center.Y - 15;
            hitbox.Width = 30;
            hitbox.Height = 30;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 4; i++)
            {
                int dustType = DustID.GemDiamond;
                Dust d = Dust.NewDustPerfect(Projectile.Center, dustType);
                d.velocity = -Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.Lerp(-MathHelper.PiOver4, MathHelper.PiOver4, i / 3f)) * 5f;
                d.noGravity = true;
            }
        }
    }
}
