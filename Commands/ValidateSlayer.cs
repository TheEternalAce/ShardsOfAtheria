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
            SoA.Log("All Slayer mode fields", "/validateSlayer command: ", true);
            SoA.Log(slayer.slayerMode, "Slayer mode enabled: ", true);
            SoA.Log(slayer.slayerSet, "Entropy set bonusactive: ", true);
            SoA.Log(slayer.soulCrystalNames, "Soul crystals: ", true);
            SoA.Log(slayer.soulTeleports, "Soul crystal teleports: ", true);
            SoA.Log(slayer.lunaticCircleFragments, "Cultist circle fragments: ", true);
        }
    }
}
