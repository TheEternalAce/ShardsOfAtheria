using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class ZenovaJavelin : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenova Javelin");
            Description.SetDefault("Defense lowered and losing life");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 26;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 26;
        }
    }

    public class ZenJavelinNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<ZenovaJavelin>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 100;
            }
        }
    }

    public class ZenJavelinPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<ZenovaJavelin>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 50 life lost per second.
                Player.lifeRegen -= 100;
            }
        }
    }
}
