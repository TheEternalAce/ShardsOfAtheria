using ShardsOfAtheria.Projectiles.Summon.Minions.GemCore;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Summons
{
    public class SwarmingAmber : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 2;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AmberFly>()] == 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
