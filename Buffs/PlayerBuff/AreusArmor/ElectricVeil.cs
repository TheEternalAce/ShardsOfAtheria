using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor
{
    public class ElectricVeil : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.thorns += 0.2f;
            player.moveSpeed += 0.2f;
            player.aggro -= 500;
        }
    }
}
