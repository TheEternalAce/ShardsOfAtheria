using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class ElecBaton : ModProjectile
    {
        public override string Texture => SoA.MeleeWeapon + "AreusBaton";

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;

            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
            if (Projectile.ai[0] < 20)
            {
                Projectile.ai[0] = 20;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += MathHelper.ToRadians(20);
            if (++Projectile.ai[0] >= 20)
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 30;
            }

            if (++Projectile.ai[1] >= 10f)
            {
                Projectile.ai[1] = 0;
                ShardsHelpers.ProjectileRing(Projectile.GetSource_FromThis(),
                    Projectile.Center, 6, 1, 1, ModContent.ProjectileType<LightningBoltFriendly>(),
                    Projectile.damage, Projectile.knockBack, Projectile.owner);
            }

            if ((Projectile.getRect().Intersects(player.getRect()) && Projectile.ai[0] > 20) || player.dead)
            {
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
