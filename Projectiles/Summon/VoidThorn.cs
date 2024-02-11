using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon
{
    public class VoidThorn : ModProjectile
    {
        int SourceTargetWhoAmI => (int)Projectile.ai[0];

        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Summon/VoidThorn_Chain";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            Projectile.AddElement(2);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(10);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.timeLeft = 10;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return target.whoAmI != SourceTargetWhoAmI;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            var npc = Main.npc[SourceTargetWhoAmI];

            Vector2 mountedCenter = npc.Center;
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

            var drawPosition = Projectile.Center;
            var remainingVectorToStart = mountedCenter - drawPosition;

            float rotation = remainingVectorToStart.ToRotation() - MathHelper.PiOver2;

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (true)
            {
                float length = remainingVectorToStart.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 25f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToStart * 32 / length;
                remainingVectorToStart = mountedCenter - drawPosition;

                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, lightColor, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }

        public override void PostDraw(Color lightColor)
        {
            Projectile.DrawBlurTrail(lightColor, ShardsHelpers.Orb);
            Projectile.DrawPrimsAfterImage(lightColor);
        }
    }
}
