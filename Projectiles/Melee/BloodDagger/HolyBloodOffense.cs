using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.BloodDagger
{
    public class HolyBloodOffense : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 3;
        }

        NPC Target;
        int timer = 12;

        public override void AI()
        {
            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Blood);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;

            if (timer > 0)
            {
                timer--;
                if (timer == 1)
                {
                    Projectile.friendly = true;
                }
            }
            else
            {
                if (Target == null)
                {
                    Projectile.velocity +=
                        new Vector2(
                            Main.rand.NextFloat(-2f, 2f),
                            Main.rand.NextFloat(-2f, 2f)
                        );
                    Target = Projectile.FindClosestNPC(null, 100);
                    return;
                }
                Projectile.Track(Target, 400);

                if (!Target.active || Target.life <= 0)
                {
                    Projectile.Kill();
                }
            }
        }
    }
}