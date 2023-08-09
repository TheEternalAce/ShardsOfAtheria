using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class SlayAll : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "slayAll";

        public override string Description
            => "Make all bosses slain";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            SoA.DownedSystem.slainValkyrie = true;
            SoA.DownedSystem.slainEOC = true;
            SoA.DownedSystem.slainBOC = true;
            SoA.DownedSystem.slainEOW = true;
            SoA.DownedSystem.slainBee = true;
            SoA.DownedSystem.slainSkull = true;
            SoA.DownedSystem.slainWall = true;
            SoA.DownedSystem.slainMechWorm = true;
            SoA.DownedSystem.slainTwins = true;
            SoA.DownedSystem.slainPrime = true;
            SoA.DownedSystem.slainPlant = true;
            SoA.DownedSystem.slainGolem = true;
            SoA.DownedSystem.slainDuke = true;
            SoA.DownedSystem.slainEmpress = true;
            SoA.DownedSystem.slainMoonLord = true;
            SoA.DownedSystem.slainSenterra = true;
            SoA.DownedSystem.slainGenesis = true;

            string slayAll = "All bosses are slain";
            SoA.LogInfo(slayAll, "/slayAll command:", true);
            ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(slayAll), Color.White, caller.Player.whoAmI);
        }
    }
}
