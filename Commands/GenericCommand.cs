using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using Terraria;
using Terraria.ID;
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
			Player player = Main.LocalPlayer;
			SlayerPlayer slayerPlayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
			SoAPlayer soaPlayer = Main.LocalPlayer.GetModPlayer<SoAPlayer>();
			SoADownedSystem soaWorld = ModContent.GetInstance<SoADownedSystem>();

			if (player.name != "The Eternal Ace")
				return;
		}
    }
}
