using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Active
{
    public class AreusBlade : ModProjectile
    {
        public List<int> BlacklistedNPCIndexes = [];

        public override string Texture => ModContent.GetInstance<AreusKnife>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();

            SoAGlobalProjectile.Metalic.Add(Type, 0.5f);
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 5;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.penetrate == 5)
            {
                var player = Main.player[Projectile.owner];
                player.MinionAttackTargetNPC = target.whoAmI;
            }

            BlacklistedNPCIndexes.Add(target.whoAmI);
            var newTarget = ShardsHelpers.FindClosestNPC(Projectile.Center, null, 500f, [.. BlacklistedNPCIndexes]);
            if (newTarget != null) Projectile.velocity = ShardsHelpers.RotateTowards(Projectile.Center, Projectile.velocity, newTarget.Center, 100);
        }
    }
}
