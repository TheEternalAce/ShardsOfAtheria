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
					for (int i = 0; i < slayer.soulCrystals.Count; i++)
					{
						slayer.soulCrystals[i] = reader.ReadInt32();
					}
					// SyncPlayer will be called automatically, so there is no need to forward this data to other clients.
					break;
				default:
					Logger.WarnFormat("ShardsOfAtheria: Unknown Message type: {0}", msgType);
					break;
			}
		}
	}
}