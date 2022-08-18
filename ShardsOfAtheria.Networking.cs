using ShardsOfAtheria.Players;
using System.IO;
using Terraria;

namespace ShardsOfAtheria
{
	// This is a partial class, meaning some of its parts were split into other files. See ExampleMod.*.cs for other portions.
	partial class ShardsOfAtheria
	{
		internal enum MessageType : byte
		{
			SyncSoulCrystals
		}

		// Override this method to handle network packets sent for this mod.
		//TODO: Introduce OOP packets into tML, to avoid this god-class level hardcode.
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			MessageType msgType = (MessageType)reader.ReadByte();

			switch (msgType)
			{
				// This message syncs ExamplePlayer.exampleLifeFruits
				case MessageType.SyncSoulCrystals:
					byte playernumber = reader.ReadByte();
					SlayerPlayer slayer = Main.player[playernumber].GetModPlayer<SlayerPlayer>();
					slayer.KingSoul = reader.ReadBoolean();
					slayer.EyeSoul = reader.ReadBoolean();
					slayer.BrainSoul = reader.ReadBoolean();
					slayer.EaterSoul = reader.ReadBoolean();
					slayer.ValkyrieSoul = reader.ReadBoolean();
					slayer.BeeSoul = reader.ReadBoolean();
					slayer.SkullSoul = reader.ReadBoolean();
					slayer.DeerclopsSoul = reader.ReadBoolean();
					slayer.WallSoul = reader.ReadBoolean();
					slayer.QueenSoul = reader.ReadBoolean();
					slayer.DestroyerSoul = reader.ReadBoolean();
					slayer.PrimeSoul = reader.ReadBoolean();
					slayer.TwinSoul = reader.ReadBoolean();
					slayer.PlantSoul = reader.ReadBoolean();
					slayer.DukeSoul = reader.ReadBoolean();
					slayer.EmpressSoul = reader.ReadBoolean();
					slayer.LunaticSoul = reader.ReadBoolean();
					slayer.LordSoul = reader.ReadBoolean();
					slayer.TimeSoul = reader.ReadBoolean();
					slayer.LandSoul = reader.ReadBoolean();
					slayer.DeathSoul = reader.ReadBoolean();
					// SyncPlayer will be called automatically, so there is no need to forward this data to other clients.
					break;
				default:
					Logger.WarnFormat("ShardsOfAtheria: Unknown Message type: {0}", msgType);
					break;
			}
		}
	}
}