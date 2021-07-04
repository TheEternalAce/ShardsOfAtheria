using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class HeartBreak : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Heart Break");
            Description.SetDefault("You cannont heal with Cross Dagger od Wand Of Healing");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SMPlayer>().heartBreak = true;
        }
    }
}
