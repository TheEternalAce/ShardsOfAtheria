using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Dusts
{
    public class AreusDust_Standard : ModDust
    {
        public override string Texture => "ShardsOfAtheria/Dusts/AreusDusts"; // If we want to use vanilla texture

        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 1f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = false; // Makes the dust have no gravity.
            dust.noLight = false; // Makes the dust emit no light.
            dust.noLightEmittence = false;
            dust.scale *= 1f; // Multiplies the dust's initial scale by 1.5.

            int desiredDustTexture = 0;
            int frameX = desiredDustTexture * 10 % 1000;
            int frameY = desiredDustTexture * 10 / 1000 * 30 + Main.rand.Next(3) * 10;
            dust.frame = new Rectangle(frameX, frameY, 8, 8);
        }

        public override bool Update(Dust dust)
        {
            float scale = 0.25f;
            Lighting.AddLight(dust.position, 0 * scale, 1 * scale, 1 * scale);
            return base.Update(dust);
        }
    }
}
