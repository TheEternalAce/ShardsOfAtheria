using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon
{
    public class BloodDart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            var target = Projectile.FindClosestNPC(null, 500);
            var player = Projectile.GetPlayerOwner();
            if (player.HasMinionAttackTargetNPC)
            {
                target = Main.npc[player.MinionAttackTargetNPC];
            }
            if (target != null)
            {
                Projectile.Track(target, 10, 10);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Projectile.GetPlayerOwner();
            if (!player.HasMinionAttackTargetNPC)
            {
                player.MinionAttackTargetNPC = target.whoAmI;
            }
        }
    }
}
