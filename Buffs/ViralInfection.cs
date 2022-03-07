using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Buffs
{
    public class BasicViralInfectionI : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/ViralInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Viral Infection I");
            Main.debuff[Type] = true;
        }
    }

    public class BasicViralInfectionII : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/ViralInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Viral Infection II");
            Main.debuff[Type] = true;
        }
    }

    public class BasicViralInfectionIII : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/ViralInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Viral Infection III");
            Main.debuff[Type] = true;
        }
    }

    public class ViralInfectedNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<BasicViralInfectionI>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 2;
            }

            if (npc.HasBuff(ModContent.BuffType<BasicViralInfectionII>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 4;
            }

            if (npc.HasBuff(ModContent.BuffType<BasicViralInfectionIII>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 8;
            }
        }
    }
}
