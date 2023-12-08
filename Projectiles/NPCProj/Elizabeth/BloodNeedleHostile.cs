﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodNeedleHostile : ModProjectile
    {
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/NPCProj/Elizabeth/BloodNeedleHostile_Chain";

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.timeLeft = 10;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (Projectile.ai[2] == 2)
            {
                SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
                Projectile.ai[2]++;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.type != ModContent.NPCType<Death>())
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<DeathBleed>(300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;

            Vector2 startPos = new(Projectile.ai[0], Projectile.ai[1]);
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

            var drawPosition = Projectile.Center;
            var remainingVectorToPlayer = startPos - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (true)
            {
                float length = remainingVectorToPlayer.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 25f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToPlayer * 14 / length;
                remainingVectorToPlayer = startPos - drawPosition;

                // Finally, we draw the texture at the coordinates
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, lightColor, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}