using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Dusts
{
    public class HardlightDust_Blue : ModDust
    {
        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            {
                float scale = 0.25f;
                Lighting.AddLight(dust.position, 0.74f * scale, 0.82f * scale, 0.95f * scale);
            }
            return base.Update(dust);
        }
    }

    public class HardlightDust_Pink : ModDust
    {
        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            {
                float scale = 0.25f;
                Lighting.AddLight(dust.position, 0.89f * scale, 0.71f * scale, 0.96f * scale);
            }
            return base.Update(dust);
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return base.GetAlpha(dust, Color.White);
        }
    }
}
