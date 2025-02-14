using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ThorSpear
{
    public class ShadowWave : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            //SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
            SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
            for (int i = 0; ++i < 100;)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Projectile.velocity.RotatedByRandom(MathHelper.PiOver4) * (1 - Main.rand.NextFloat(0.40f)));
                dust.noGravity = true;
                dust.fadeIn = 1.2f;
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width *= 50;
            hitbox.Height *= 50;
            hitbox.X -= hitbox.Width / 2 - Projectile.width / 2;
            hitbox.Y -= hitbox.Height / 2 - Projectile.height / 2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
        }
    }
}
