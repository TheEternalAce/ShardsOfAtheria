using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class MendingTopaz : ModBuff
    {
    }

    public class MendingTopazPlayer : ModPlayer
    {
        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff<MendingTopaz>())
            {
                Player.lifeRegen += 20;
            }
        }
    }
}
