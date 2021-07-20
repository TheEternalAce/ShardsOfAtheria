using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class Bonked : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bonked!");
            Description.SetDefault("Defense lowered\n" +
                "'Go to bonk jail'");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 5;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 5;
        }
    }
}
