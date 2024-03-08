using Terraria;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Players
{
    public class ScreenShakePlayer : ModPlayer
    {
        public override void ModifyScreenPosition()
        {
            var shake = ScreenShake.Current;
            int intensity = 0;
            for (int i = 0; i < shake.Length; i++)
            {
                intensity += shake[i].Intensity;
                if (shake[i].Intensity > 0)
                {
                    shake[i].Intensity--;
                    shake[i].Duration--;
                }
            }
            var shaking = (Main.rand.NextVector2Circular(intensity, intensity) * 0.5f).Floor(); ;
            Main.screenPosition += shaking * SoA.ClientConfig.screenShakeIntensity;
            Main.screenPosition = Main.screenPosition.Floor();
        }
    }
}
