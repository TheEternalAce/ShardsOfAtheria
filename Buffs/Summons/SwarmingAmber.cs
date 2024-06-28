using ShardsOfAtheria.Projectiles.Summon.Minions.GemCore;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Summons
{
    public class SwarmingAmber : ModBuff
    {
        public override void SetStaticDefaults()
        {
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.InCombat())
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }

            if (player.ownedProjectileCounts[ModContent.ProjectileType<AmberFly>()] == 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
