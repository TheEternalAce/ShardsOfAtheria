using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ShardsUI.InfoDisplays
{
    public class PrideData : InfoDisplay
    {
        public override string Texture => SoA.SinDataTexture;

        public override bool Active() => Main.LocalPlayer.Sinner().sinID == SinnerPlayer.PRIDE;

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            var player = Main.LocalPlayer;
            if (!player.InCombat()) displayColor = Color.Gray;
            var pride = player.Sinner();
            return pride.attacksHit + "/" + pride.attacksMade;
        }
    }
}
