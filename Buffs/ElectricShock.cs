using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class ElectricShock : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Shock");
            Description.SetDefault("Moving hurts");
            Main.debuff[Type] = true;
        }
    }

    public class ShockedNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<ElectricShock>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 20;
            }
        }
    }

    public class ShockedPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<ElectricShock>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes .5 life lost per second.
                if (Main.keyState.IsKeyDown(Keys.Left) || Main.keyState.IsKeyDown(Keys.Right))
                    Player.lifeRegen -= 20;
                else Player.lifeRegen -= 2;
            }
        }
    }
}
