using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
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
            ShardsDownedSystem downedSystem = SoA.DownedSystem;

            shards.genesisRagnarockUpgrades = 0;
            string arsenalReset = "Reset Genesis and Ragnarok upgrades";
            ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(arsenalReset), Color.White, player.whoAmI);
            SoA.LogInfo(arsenalReset, "/generic command:", true);
            shards.areusRod = false;
            string areusRodReset = "Disabled Areus Rod effects";
            ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(areusRodReset), Color.White, player.whoAmI);
            SoA.LogInfo(areusRodReset, "/generic command:", true);
        }
    }
}
