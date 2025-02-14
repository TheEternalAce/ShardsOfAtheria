using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Star
{
    public class RedStarSmall : ModProjectile
    {
        Projectile ParentStar => Main.projectile[(int)Projectile.ai[0]];

        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 30;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(15) * Projectile.direction;
            Projectile.spriteDirection = Projectile.direction;
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 16; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemRuby, speed * 2.4f);
                d.noGravity = true;
            }
            for (var i = 0; i < 4; i++)
            {
                Vector2 speed = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmethyst, speed * 1f);
                d.noGravity = true;
            }
            var player = Projectile.GetPlayerOwner();
            if (player.IsLocal())
            {
                var position = Main.MouseWorld;
                int npcWhoAmI = Projectile.FindTargetWithLineOfSight(1000);
                if (player.HasMinionAttackTargetNPC) npcWhoAmI = player.MinionAttackTargetNPC;
                if (npcWhoAmI != -1) position = Main.npc[npcWhoAmI].Center;
                var vector = position - Projectile.Center;
                vector.Normalize();
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector, ModContent.ProjectileType<RedStarShot>(),
                    (int)(Projectile.damage * (1f + player.Slayer().artifactExtraSummons * 0.15f)), Projectile.knockBack);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawBloomTrail(lightColor.UseA(100), SoA.OrbBloom, scale: 0.5f);
            return base.PreDraw(ref lightColor);
        }
    }
}
