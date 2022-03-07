using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Buffs
{
    public class BasicBacterialInfectionI : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/BacterialInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Bacterial Infection I");
            Main.debuff[Type] = true;
        }
    }

    public class BasicBacterialInfectionII : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/BacterialInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Bacterial Infection II");
            Main.debuff[Type] = true;
        }
    }

    public class BasicBacterialInfectionIII : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/BacterialInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Bacterial Infection III");
            Main.debuff[Type] = true;
        }
    }

    public class BacterialInfectedNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<BasicBacterialInfectionI>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 2;
            }

            if (npc.HasBuff(ModContent.BuffType<BasicBacterialInfectionII>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 4;
            }

            if (npc.HasBuff(ModContent.BuffType<BasicBacterialInfectionIII>()))
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
