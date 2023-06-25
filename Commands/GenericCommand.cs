using ShardsOfAtheria.Players;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class GenericCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "generic";
        // /generic

        public override string Description
            => "Used by the mod developers to help with certain features";

        //create a break point on the bellow line, run the command, then make the effect
        //Reset before debugging
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = caller.Player;
            SlayerPlayer slayer = player.Slayer();
            ShardsPlayer shards = player.Shards();
            ShardsDownedSystem soaWorld = SoA.DownedSystem;

            shards.genesisRagnarockUpgrades = 0;
            SoA.Log("/generic command:", "Reset Genesis and Ragnarok upgrades", true);
            shards.areusRod = false;
            SoA.Log("/generic command:", "Disabled Areus Rod effects", true);
        }
    }
}
