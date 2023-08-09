using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class ResetSlayer : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "resetSlayer";

        public override string Description
            => "Reset Slayer mode related variables";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var sPlayer = caller.Player.Slayer();

            sPlayer.soulCrystalNames.Clear();
            SoA.DownedSystem.slainKing = false;
            SoA.DownedSystem.slainEOC = false;
            SoA.DownedSystem.slainBOC = false;
            SoA.DownedSystem.slainEOW = false;
            SoA.DownedSystem.slainValkyrie = false;
            SoA.DownedSystem.slainBee = false;
            SoA.DownedSystem.slainSkull = false;
            SoA.DownedSystem.slainDeerclops = false;
            SoA.DownedSystem.slainWall = false;
            SoA.DownedSystem.slainQueen = false;
            SoA.DownedSystem.slainMechWorm = false;
            SoA.DownedSystem.slainTwins = false;
            SoA.DownedSystem.slainPrime = false;
            SoA.DownedSystem.slainPlant = false;
            SoA.DownedSystem.slainGolem = false;
            SoA.DownedSystem.slainDuke = false;
            SoA.DownedSystem.slainEmpress = false;
            SoA.DownedSystem.slainLunatic = false;

            SoA.DownedSystem.slainPillarNebula = false;
            SoA.DownedSystem.slainPillarSolar = false;
            SoA.DownedSystem.slainPillarStardust = false;
            SoA.DownedSystem.slainPillarVortex = false;

            SoA.DownedSystem.slainMoonLord = false;
            SoA.DownedSystem.slainSenterra = false;
            SoA.DownedSystem.slainGenesis = false;
            SoA.DownedSystem.slainDeath = false;

            string slayerReset = "Slayer mode reset";
            SoA.LogInfo(slayerReset, "/resetSlayer command:", true);
            ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(slayerReset), Color.White, caller.Player.whoAmI);
        }
    }
}
