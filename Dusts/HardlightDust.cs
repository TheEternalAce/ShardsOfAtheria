using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Dusts
{
    public class HardlightDust_Blue : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 1f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = false; // Makes the dust have no gravity.
            dust.noLight = false; // Makes the dust emit no light.
            dust.scale *= 1f; // Multiplies the dust's initial scale by 1.5.
        }

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
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 1f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = false; // Makes the dust have no gravity.
            dust.noLight = false; // Makes the dust emit no light.
            dust.scale *= 1f; // Multiplies the dust's initial scale by 1.5.
        }

        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            {
                float scale = 0.25f;
                Lighting.AddLight(dust.position, 0.89f * scale, 0.71f * scale, 0.96f * scale);
            }
            return base.Update(dust);
        }
    }
}
