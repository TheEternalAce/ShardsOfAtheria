using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class Overheat : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Overheat");
            Description.SetDefault("You cannont avtivate Overdrive");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SMPlayer>().overheat = true;
        }
    }
}
