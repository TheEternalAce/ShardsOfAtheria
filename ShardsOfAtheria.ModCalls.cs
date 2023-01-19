using Microsoft.Xna.Framework;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
	partial class ShardsOfAtheria
	{
		// The following code allows other mods to "call" Example Mod data.
		// This allows mod developers to access Example Mod's data without having to set it a reference.
		// Mod calls are not exposed by default, so it will be up to you to publish appropriate calls for your mod, and what values they return.
		public override object Call(params object[] args)
		{
			// Make sure the call doesn't include anything that could potentially cause exceptions.
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
			}

			if (args.Length == 0)
			{
				throw new ArgumentException("Arguments cannot be empty!");
			}

			// This check makes sure that the argument is a string using pattern matching.
			// Since we only need one parameter, we'll take only the first item in the array..
			if (args[0] is string content)
			{
				// ..And treat it as a command type.
				switch (content)
				{
					default:
						throw new ArgumentException("Unrecognized ModCall. Usable ModCalls for Shards of Atheria are as follows: " +
							"checkSlayer, checkSlainBoss, addSlainBoss, addNecronomiconEntry, addColoredNecronomiconEntry, wipNecronomiconEntry, and addSoulCrystal.");
					case "checkSlayer":
						// Checks if the player has Slayer Mode enabled
						if (args[1] is Player slayer)
						{
							return slayer.GetModPlayer<SlayerPlayer>().slayerMode;
						}
						else
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid Player type.");
						}
					case "checkSlainBoss":
						// Check if a boss has been slain
						if (args[1] is int bossType)
						{
							return ShardsDownedSystem.slainBosses.Contains(bossType);
						}
						else
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid int.");
						}
					case "addSlainBoss":
						// Check if a boss has been slain
						if (args[1] is int boss)
						{
							ShardsDownedSystem.slainBosses.Add(boss);
						}
						else
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid int.");
						}
						break;
					case "addNecronomiconEntry":
						if (args[1] is not string) // Mod Name
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid string.");
						}
						if (args[2] is not string) // Boss Name
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a valid string.");
						}
						if (args[3] is not string) // Soul Crystal Tooltip
						{
							throw new ArgumentException(args[3].GetType().Name + " is not a valid string.");
						}
						if (args[4] is not int) // Soul Crystal Item ID
						{
							throw new ArgumentException(args[4].GetType().Name + " is not a valid int.");
						}
						else
						{
							Entry.NewEntry((string)args[1], (string)args[2], (string)args[3], (int)args[4]);
						}
						break;
					case "addColoredNecronomiconEntry":
						if (args[1] is not string) // Mod Name
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid string.");
						}
						if (args[2] is not string) // Boss Name
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a valid string.");
						}
						if (args[3] is not string) // Soul Crystal Tooltip
						{
							throw new ArgumentException(args[3].GetType().Name + " is not a valid string.");
						}
						if (args[4] is not Color)
						{
							throw new ArgumentException(args[4].GetType().Name + " is not a valid color.");
						}
						if (args[5] is not int) // Soul Crystal Item ID
						{
							throw new ArgumentException(args[5].GetType().Name + " is not a valid int.");
						}
						else
						{
							Entry.NewEntry((string)args[1], (string)args[2], (string)args[3], (Color)args[4], (int)args[5]);
						}
						break;
					case "wipNecronomiconEntry":
						return Entry.WipEntry();
					case "overcharge":
						if (args[1] is Player ocPlayer)
						{
							if (args[2] is float chargeToAdd)
							{
								ocPlayer.GetModPlayer<OverchargePlayer>().overcharge += chargeToAdd;
								break;
							}
							else if (args[2] is bool)
							{
								ocPlayer.GetModPlayer<OverchargePlayer>().overcharge = 0;
								break;
							}
							{
								return ocPlayer.GetModPlayer<OverchargePlayer>().overcharge;
							}
						}
						else
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a valid Player type.");
						}
					case "overcharged":
						if (args[1] is Player ocedPlayer)
						{
							return ocedPlayer.GetModPlayer<OverchargePlayer>().overcharged;
						}
						else if (args[1] is Projectile ocedProj)
						{
							if (args[2] is bool oced)
							{
								ocedProj.GetGlobalProjectile<OverchargedProjectile>().overcharged = oced;
								break;
							}
							else
							{
								return ocedProj.GetGlobalProjectile<OverchargedProjectile>().overcharged;
							}
						}
						else
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a valid Player type.");
						}
					case "invoke":
						if (args[1] is string str)
						{
							switch (str)
							{
								case "callStorm":
									if (args[2] is Projectile stormProj)
									{
										if (args[3] is int lightningAmount)
										{
											stormProj.CallStorm(lightningAmount);
										}
										else
										{
											stormProj.CallStorm(3);
										}
									}
									else
									{
										throw new ArgumentException(args[2].GetType().Name + " is not a valid Projectile type.");
									}
									break;
							}
							break;
						}
						else
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a valid string.");
						}
					case "checkHasSoulCrystal":
						if (args[1] is Player soulsPlayer)
						{
							if (args[2] is int sC)
							{
								return soulsPlayer.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(sC);
							}
							else
							{
								throw new ArgumentException(args[2].GetType().Name + " is not a valid int.");
							}
						}
						else
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid SlayerPlayer type.");
						}
					case "addSoulCrystal":
						if (args[1] is Player soulsPlayer2)
						{
							if (args[2] is int)
							{
								soulsPlayer2.GetModPlayer<SlayerPlayer>().soulCrystals.Add((int)args[3]);
							}
							else
							{
								throw new ArgumentException(args[3].GetType().Name + " is not a valid int.");
							}
						}
						else
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a Player type.");
						}
						break;
					case "checkSoulConfig":
						return ModContent.GetInstance<ShardsClientConfig>().instantAbsorb;
				}
			}

			// If the arguments provided don't match anything we wanted to return a value for, we'll return a 'false' boolean.
			// This value can be anything you would like to provide as a default value.
			return false;
		}
	}
}
