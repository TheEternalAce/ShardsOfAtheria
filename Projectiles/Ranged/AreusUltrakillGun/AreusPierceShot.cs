using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.AreusUltrakillGun
{
    public class AreusPierceShot : BasicBeam
    {
        public override string Texture => SoA.BlankTexture;

        public override int DustType => DustID.Electric;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(7);
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
            Projectile.aiStyle = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 3;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<ElectricShock>(300);
            if (target.life >= 1000 && target.CanBeChasedBy())
            {
                var copyHit = hit;
                for (int i = 0; i < Projectile.penetrate - 1; i++)
                {
                    copyHit.Damage = (int)(hit.Damage * 0.75f);
                    target.StrikeNPC(copyHit);
                }
                Projectile.Kill();
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<ElectricShock>(300);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
