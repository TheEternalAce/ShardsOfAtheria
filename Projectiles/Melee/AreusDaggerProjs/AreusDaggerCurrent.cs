using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusDaggerProjs
{
    public class AreusDaggerCurrent : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 20;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee.TryThrowing();
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.ai[0] = GetStuckDaggers(target);
            modifiers.ScalingBonusDamage += 0.15f * GetStuckDaggers(target, true);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var npc = ShardsHelpers.FindClosestNPC(Projectile.Center, (NPC npc) => GetStuckDaggers(npc) > 0, 200f, target.whoAmI);

            if (npc == null || Projectile.ai[0] == 0) Projectile.Kill();
            else Projectile.velocity = Projectile.Center.DirectionTo(npc.Center);
        }

        static int GetStuckDaggers(NPC target, bool killDaggers = false)
        {
            int stuckDaggers = 0;
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.ModProjectile is AreusDaggerProj dagger &&
                    projectile.active &&
                    dagger.IsStickingToTarget &&
                    dagger.TargetWhoAmI == target.whoAmI)
                {
                    stuckDaggers++;
                    if (killDaggers) projectile.Kill();
                }
            }
            return stuckDaggers;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;
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
    }
}
