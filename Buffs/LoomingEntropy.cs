using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class LoomingEntropy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Looming Entropy");
            Description.SetDefault("'Heat death is Swiftly approaching..'\n" +
                "Defense reduced and losing life");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 14;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 14;
        }
    }

    public class EntropyPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<LoomingEntropy>()))
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 50;
            }
        }
    }

    public class EntropyNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<LoomingEntropy>()))
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 50;
            }
        }
    }
}
