using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class BaseExploration : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Knowledge Base: Exploration");
            Description.SetDefault("10% increased movement speed and max run speed\n" +
                "Mining, Builder, Shine, Night Owl, Dangersense, Hunter and Calming potions effects\n" +
                "'You are efficent with your resources'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Lighting.AddLight(player.Center, TorchID.White);
            player.nightVision = true;
            player.dangerSense = true;
            player.calmed = true;
        }
    }
}
