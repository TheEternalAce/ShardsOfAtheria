using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusSpears
{
    public class PartisanHead : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus(true);
            Projectile.AddRedemptionElement(9);

            SoAGlobalProjectile.Metalic.Add(Type, 1f);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            AIType = ProjectileID.BoneJavelin;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 20;
            Projectile.usesLocalNPCImmunity = true;
        }

        // Change this number if you want to alter how the alpha changes
        private const int ALPHA_REDUCTION = 25;

        int gravityTimer = 0;
        public override void AI()
        {
            // Slowly remove alpha as it is present
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= ALPHA_REDUCTION;
            }

            // If alpha gets lower than 0, set it to 0
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            Projectile.SetVisualOffsets(38);
            Projectile.ApplyGravity(ref gravityTimer);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust_Dark>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }
    }
}
