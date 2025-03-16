using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.Baton
{
    public class ElecBaton : ModProjectile
    {
        public override string Texture => SoA.MeleeWeapon + "AreusBaton";

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(5);
        }

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
            SoundEngine.PlaySound(SoundID.Item91, Projectile.Center);
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
            foreach (var npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && npc.whoAmI != target.whoAmI)
                {
                    if (Projectile.Distance(npc.Center) < 100)
                    {
                        NPC.HitInfo hitInfo = new()
                        {
                            Damage = Projectile.damage,
                            Knockback = 0,
                            DamageType = Projectile.DamageType,
                            HitDirection = Projectile.direction,
                        };
                        npc.StrikeNPC(hitInfo);
                    }
                }
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += MathHelper.ToRadians(20);
            if (++Projectile.ai[0] >= 20)
            {
                Projectile.Track(player.Center, 30, 15);
            }

            if (Projectile.getRect().Intersects(player.getRect()) && Projectile.ai[0] > 20 || player.dead)
            {
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            int r = lightColor.R + SoA.ElectricColor.R;
            int g = lightColor.G + SoA.ElectricColor.G;
            int b = lightColor.B + SoA.ElectricColor.B;
            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            lightColor = new(r, g, b);
            return base.PreDraw(ref lightColor);
        }
    }
}
