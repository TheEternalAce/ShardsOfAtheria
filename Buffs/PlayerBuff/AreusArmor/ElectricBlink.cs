using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor
{
    public class ElectricBlink : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.manaRegen += 200;
            player.manaCost -= 0.8f;
        }
    }
}
