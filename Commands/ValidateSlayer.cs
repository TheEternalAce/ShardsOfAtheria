using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using static ShardsOfAtheria.Utilities.Entry;

namespace ShardsOfAtheria.Commands
{
    class Slayer : ModCommand
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShardsOfAtheria.DeveloperMode;
        }

        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "validateSlayer";

        public override string Description
            => "Check and validate Slayer mode related variables";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();

            ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("" + slayer.soulCrystals.Count), Color.White, Main.LocalPlayer.whoAmI);
            for (int i = 0; i < entries.Count; i++)
            {
                PageEntry entry = entries[i];
                if (slayer.soulCrystals.Contains(entry.crystalItem))
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(entry.entryName), Color.White, Main.LocalPlayer.whoAmI);
                }
            }
        }
    }
}
