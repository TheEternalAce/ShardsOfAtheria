using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class EfficientAmethyst : ModBuff
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

            for (int i = 0; i < player.CountBuffs(); i++)
            {
                int buffID = player.buffType[i];
                if (!Main.debuff[buffID] && !BuffID.Sets.TimeLeftDoesNotDecrease[buffID] && !Main.buffNoTimeDisplay[buffID])
                {
                    player.buffTime[i] += 1;
                    if (player.buffTime[i] < 60)
                    {
                        player.buffTime[i] = 60;
                    }
                }
            }
        }
    }

    public class RallyingAmethystPlayer : ModPlayer
    {
        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff<MendingTopaz>() && Player.HasBuff<EfficientAmethyst>())
            {
                Player.lifeRegen += 20;
            }
        }
    }
}
