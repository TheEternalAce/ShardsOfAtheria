using SagesMania.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class ZenovaJavelin : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Zenova Javelin");
            Description.SetDefault("Defense lowered and losing life");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 26;
            player.GetModPlayer<SMPlayer>().zenovaJavelin = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 26;
            npc.GetGlobalNPC<SMGlobalNPC>().zenovaJavelin = true;
        }
    }
}
