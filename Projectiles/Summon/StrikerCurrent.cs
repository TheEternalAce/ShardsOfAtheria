using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon
{
    public class StrikerCurrent : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;
            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 3;
        }

        NPC Target;
        int targetWhoAmI
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return target.whoAmI == targetWhoAmI;
        }

        public override void AI()
        {
            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;

            if (Target == null)
            {
                Target = Main.npc[targetWhoAmI];
                return;
            }
            Projectile.Track(Target, 400);

            if (!Target.CanBeChasedBy())
            {
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}