using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusJustitia
{
    public class FlameStabExtention : AreusJustitia_Stab
    {
        public override string Texture => ModContent.GetInstance<AreusJustitia_Stab>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(3, 9);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(13);
        }

        public override void AI()
        {
            BaseAI();

            for (int i = 0; i < 2; i++)
            {
                int type = Main.rand.NextBool(4) ? DustID.Electric : DustID.Torch;
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, type);
                dust.noGravity = true;
                if (dust.type == DustID.Torch) dust.fadeIn = 1.3f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
            target.AddBuff(BuffID.OnFire3, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.Firebrick.UseA(0);
            return true;
        }
    }
}
