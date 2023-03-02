using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Dusts
{
    public class AreusDusts : ModDust
    {
        internal int colorType;
        internal Vector3 color;

        public override string Texture => "ShardsOfAtheria/Dusts/AreusDusts"; // If we want to use vanilla texture

        public override void OnSpawn(Dust dust)
        {
            int desiredDustTexture = colorType;
            int frameX = desiredDustTexture * 10 % 1000;
            int frameY = desiredDustTexture * 10 / 1000 * 30 + Main.rand.Next(3) * 10;
            dust.frame = new Rectangle(frameX, frameY, 8, 8);
        }

        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, color);
            return base.Update(dust);
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return base.GetAlpha(dust, Color.White);
        }
    }

    public class AreusDust : AreusDusts
    {
        public override void OnSpawn(Dust dust)
        {
            colorType = 0;
            float scale = 0.25f;
            color = new(0 * scale, 1 * scale, 1 * scale);
            base.OnSpawn(dust);
        }
    }

    public class AreusDust_Gray : AreusDusts
    {
        public override void OnSpawn(Dust dust)
        {
            colorType = 1;
            float scale = 0.25f;
            color = new(1 * scale, 1 * scale, 1 * scale);
            base.OnSpawn(dust);
        }
    }

    public class AreusDust_Red : AreusDusts
    {
        public override void OnSpawn(Dust dust)
        {
            colorType = 2;
            float scale = 0.25f;
            color = new(1 * scale, 0 * scale, 0 * scale);
            base.OnSpawn(dust);
        }
    }

    public class AreusDust_Yellow : AreusDusts
    {
        public override void OnSpawn(Dust dust)
        {
            colorType = 3;
            float scale = 0.25f;
            color = new(1 * scale, 1 * scale, 0 * scale);
            base.OnSpawn(dust);
        }
    }
}
