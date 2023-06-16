using ShardsOfAtheria.Players;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class ValidateSlayer : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "validateSlayer";

        public override string Description
            => "Check and validate Slayer mode related variables";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            SlayerPlayer slayer = caller.Player.GetModPlayer<SlayerPlayer>();
            SoA.Log("/validateSlayer command: ", "All Slayer mode fields", true);
            SoA.Log("Slayer mode enabled: ", slayer.slayerMode, true);
            SoA.Log("Entropy set bonusactive: ", slayer.slayerSet);
            SoA.Log("Soul crystals: ", slayer.soulCrystals);
            SoA.Log("Soul crystal teleports: ", slayer.soulTeleports);
            SoA.Log("Cultist circle fragments: ", slayer.lunaticCircleFragments);
        }
    }
}
