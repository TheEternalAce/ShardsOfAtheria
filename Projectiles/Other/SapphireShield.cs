using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class SapphireShield : ModProjectile
    {
        public static Asset<Texture2D> glowmask;

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture);
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.timeLeft = 35;
            Projectile.tileCollide = false;
            Projectile.alpha = 100;
        }

        public override void AI()
        {
            int frameSpeed = 6;
            if (++Projectile.frameCounter >= frameSpeed)
            {
                if (Projectile.frame < Main.projFrames[Type] - 1)
                {
                    Projectile.frame++;
                }
            }
            Player owner = Main.player[Projectile.owner];
            Projectile.Center = owner.Center;
            owner.immuneNoBlink = true;

            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.5f), Projectile.Center);
                //SoundEngine.PlaySound(SoundID.Item37.WithPitchOffset(-0.5f), Projectile.Center);
                //SoundEngine.PlaySound(SoundID.NPCHit4.WithPitchOffset(-0.5f), Projectile.Center);
                Projectile.ai[0] = 1;
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Projectile.hide = true;
            overPlayers.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}