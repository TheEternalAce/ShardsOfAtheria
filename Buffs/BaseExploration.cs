using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class BaseExploration : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Knowledge Base: Exploration");
            Description.SetDefault("10% increased movement speed and max run speed\n" +
                "Mining, Builder, Shine, Night Owl, Dangersense, Hunter and Calming potions effects\n" +
                "'You are efficent with your resources'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxRunSpeed = +.1f;
            player.nightVision = true;
            player.dangerSense = true;
            player.calmed = true;
            player.GetModPlayer<SMPlayer>().baseExploration = true;
        }
    }
}
