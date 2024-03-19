using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.AreusBannerBuffs
{
    public class AreusBannerMeleeBuff : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.Areus().bannerDamage = true;
            player.Areus().bannerDefense = true;
            player.Areus().bannerEndurance = true;
        }
    }
}
