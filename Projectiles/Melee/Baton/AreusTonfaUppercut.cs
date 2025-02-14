using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.Baton
{
    public class AreusTonfaUppercut : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.MakeTrueMelee();
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(18);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 10;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
        }

        public override void AI()
        {
            Projectile.SetVisualOffsets(50);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = -MathHelper.PiOver4;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            //var texture = ModContent.Request<Texture2D>("");
            return base.PreDraw(ref lightColor);
        }
    }
}