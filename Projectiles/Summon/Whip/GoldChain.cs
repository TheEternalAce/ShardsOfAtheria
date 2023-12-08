﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Whip
{
    public class GoldChain : ModProjectile
    {
        int SourceTargetWhoAmI => (int)Projectile.ai[0];

        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Summon/Whip/GoldChain_Link";

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 30;
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
                if (length < 14f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToStart * 14 / length;
                remainingVectorToStart = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
