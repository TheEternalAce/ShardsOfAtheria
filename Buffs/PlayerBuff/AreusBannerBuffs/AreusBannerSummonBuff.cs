using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.AreusBannerBuffs
{
    public class AreusBannerSummonBuff : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.Areus().bannerRegen = true;
            player.Areus().bannerMobility = true;
            player.Areus().bannerDefense = true;
        }
    }
}
