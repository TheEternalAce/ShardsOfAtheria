using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class Infection : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infected");
            Description.SetDefault("You are succumbing to the infection...");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 16;
            player.GetDamage(DamageClass.Generic) *= 2;
            player.moveSpeed /= 2;
        }
    }

    public class InfectedPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<Infection>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 5 life lost per second.
                Player.lifeRegen -= 10;
            }
        }
    }

    public class InfectedNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<Infection>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 10;
            }
        }
    }
}
