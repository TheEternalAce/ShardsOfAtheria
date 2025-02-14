using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.AreusBannerBuffs
{
    public class AreusBannerThrowingBuff : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.Areus().bannerMobility = true;
            player.Areus().bannerVelocity = true;
            player.Areus().bannerResourceManagement = true;
        }
    }
}
