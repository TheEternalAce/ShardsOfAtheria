using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria.Chat;
using Terraria.Localization;
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
            ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("See log for detailed info"), Color.White, caller.Player.whoAmI);
            SoA.LogInfo("All Slayer mode fields", "/validateSlayer command: ", true);
            SoA.LogInfo(slayer.slayerMode, "Slayer mode enabled: ", true);
            SoA.LogInfo(slayer.slayerSet, "Entropy set bonusactive: ", true);
            SoA.LogInfo(slayer.soulCrystalNames, "Soul crystals: ", true);
            SoA.LogInfo(slayer.soulTeleports, "Soul crystal teleports: ", true);
            SoA.LogInfo(slayer.lunaticCircleFragments, "Cultist circle fragments: ", true);
        }
    }
}
