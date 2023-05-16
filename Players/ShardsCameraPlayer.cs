using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Players
{
    public class ShardsCameraPlayer : ModPlayer
    {
        public int sceneInvulnerability = 0;

        public override void ResetEffects()
        {
            if (sceneInvulnerability > 0)
            {
                sceneInvulnerability--;
            }
        }

        public override void ModifyScreenPosition()
        {
            ModContent.GetInstance<CameraFocus>().UpdateScreen(this);
            Main.screenPosition = Main.screenPosition.Floor();
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (sceneInvulnerability > 0)
            {
                return true;
            }
            return base.FreeDodge(info);
        }
    }
}
