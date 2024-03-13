using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Dusts
{
    public class AreusDust_Dark : ModDust
    {
        Vector3 color;

        public override void OnSpawn(Dust dust)
        {
            float scale = 0.25f;
            color = new(0 * scale, 1 * scale, 1 * scale);
            int desiredDustTexture = 0;
            int frameX = desiredDustTexture * 10 % 1000;
            int frameY = desiredDustTexture * 10 / 1000 * 30 + Main.rand.Next(3) * 10;
            dust.frame = new Rectangle(frameX, frameY, 8, 8);
        }

        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            {
                Lighting.AddLight(dust.position, color);
            }
            return base.Update(dust);
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return base.GetAlpha(dust, Color.White);
        }
    }
}
