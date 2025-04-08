using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AmbassadorBeam : BasicBeam
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
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, Type + 1, Projectile.damage, 0f);
        }
    }
}