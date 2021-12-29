using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class HeartBreak : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart Break");
            Description.SetDefault("You cannot heal with Cross Dagger or Wand Of Healing");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SMPlayer>().heartBreak = true;
        }
    }
}
