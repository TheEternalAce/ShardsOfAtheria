using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class ElectricMarksmanShot : ModProjectile
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
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.arrow = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item72);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;
            if (++Projectile.ai[0] >= 5)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }
    }
}
