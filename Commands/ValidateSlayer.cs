using ShardsOfAtheria.Utilities;
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
            var slayer = caller.Player.Slayer();
            SoA.Log("/validateSlayer command: ", "All Slayer mode fields", true);
            SoA.Log("Slayer mode enabled: ", slayer.slayerMode, true);
            SoA.Log("Entropy set bonusactive: ", slayer.slayerSet, true);
            SoA.Log("Soul crystals: ", slayer.soulCrystalNames, true);
            SoA.Log("Soul crystal teleports: ", slayer.soulTeleports, true);
            SoA.Log("Cultist circle fragments: ", slayer.lunaticCircleFragments, true);
        }
    }
}
