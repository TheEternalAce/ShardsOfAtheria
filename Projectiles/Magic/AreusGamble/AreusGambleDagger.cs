using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic.ElecGauntlet;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.AreusGamble
{
    public class AreusGambleDagger : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<ElecDagger>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementElec();
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 360;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

        public override void AI()
        {
            Projectile.SetVisualOffsets(new Vector2(22, 28));
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 300);
            Projectile.hostile = true;
            var player = Projectile.GetPlayerOwner();
            var vector = player.Center - Projectile.Center;
            vector.Normalize();
            vector *= 16f;
            Projectile.velocity = vector;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 120);
        }
    }
}
