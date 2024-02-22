using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
    partial class SoA
    {
        #region Commands
        public const string FlagSlayer = "checkSlayer";
        public const string FlagSlainBoss = "checkSlainBoss";
        public const string FlagCombat = "checkCombat";
        public const string FlagHasSoulCrystal = "checkHasSoulCrystal";
        public const string FlagSoulCrystalConfig = "checkSoulConfig";

        public const string AddSoulCrystal = "addSoulCrystal";
        public const string WIPNecronomicon = "wipNecronomiconEntry";
        public const string AddNecronomicon = "addNecronomiconEntry";
        public const string AddNecronomiconColor = "addColoredNecronomiconEntry";
        public const string AddSlainBoss = "addSlainBoss";

        public const string AddMagneticProjectile = "addMagnetProj";

        public const string InvalidPlayer = " is not a valid Player type.";
        public const string InvalidBool = " is not a valid bool.";
        public const string InvalidInt = " is not a valid int.";
        public const string InvalidFloat = " is not a valid float.";
        public const string InvalidProjectile = " is not a valid Projectile type.";
        public const string InvalidString = " is not a valid string.";
        public const string InvalidMod = " is not a valid mod.";
        public const string InvalidColor = " is not a valid color.";
        #endregion

        public override object Call(params object[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
            }

            if (args.Length == 0)
            {
                throw new ArgumentException("Arguments cannot be empty!");
            }

            if (args[0] is string content)
            {
                switch (content)
                {
                    default:
                        throw new ArgumentException("Unrecognized ModCall.");
                    case FlagSlayer:
                        if (args[1] is Player slayer)
                        {
                            return slayer.Slayer().slayerMode;
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidPlayer);
                        }
                    case FlagSlainBoss:
                        if (args[1] is int bossType)
                        {
                            return ShardsDownedSystem.slainBosses.Contains(bossType);
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidInt);
                        }
                    case FlagCombat:
                        if (args[1] is Player combatPlayer)
                        {
                            return combatPlayer.Shards().InCombat;
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidPlayer);
                        }
                    case AddSlainBoss:
                        if (args[1] is int boss)
                        {
                            ShardsDownedSystem.slainBosses.Add(boss);
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidInt);
                        }
                        break;
                    case AddNecronomicon:
                        if (args[1] is not Mod)
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidMod);
                        }
                        if (args[2] is not string)
                        {
                            throw new ArgumentException(args[2].GetType().Name + InvalidString);
                        }
                        if (args[3] is not string)
                        {
                            throw new ArgumentException(args[3].GetType().Name + InvalidString);
                        }
                        if (args[4] is not string)
                        {
                            throw new ArgumentException(args[4].GetType().Name + InvalidString);
                        }
                        else
                        {
                            var mod = (Mod)args[1];
                            Entry.NewEntry(mod.Name, (string)args[2], (string)args[3], (string)args[4]);
                        }
                        break;
                    case AddNecronomiconColor:
                        if (args[1] is not Mod)
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidMod);
                        }
                        if (args[2] is not string)
                        {
                            throw new ArgumentException(args[2].GetType().Name + InvalidString);
                        }
                        if (args[3] is not string)
                        {
                            throw new ArgumentException(args[3].GetType().Name + InvalidString);
                        }
                        if (args[4] is not Color)
                        {
                            throw new ArgumentException(args[4].GetType().Name + InvalidColor);
                        }
                        if (args[5] is not string)
                        {
                            throw new ArgumentException(args[5].GetType().Name + InvalidString);
                        }
                        else
                        {
                            var mod = (Mod)args[1];
                            Entry.NewEntry(mod.Name, (string)args[2], (string)args[3], (Color)args[4], (string)args[5]);
                        }
                        break;
                    case WIPNecronomicon:
                        return Entry.WipEntry();
                    case FlagHasSoulCrystal:
                        if (args[1] is Player soulsPlayer)
                        {
                            if (args[2] is string item)
                            {
                                return soulsPlayer.Slayer().soulCrystalNames.Contains(item);
                            }
                            else
                            {
                                throw new ArgumentException(args[2].GetType().Name + InvalidInt);
                            }
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidPlayer);
                        }
                    case AddSoulCrystal:
                        if (args[1] is Player soulsPlayer2)
                        {
                            if (args[2] is string item)
                            {
                                soulsPlayer2.Slayer().soulCrystalNames.Add(item);
                                break;
                            }
                            else
                            {
                                throw new ArgumentException(args[2].GetType().Name + InvalidString);
                            }
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidPlayer);
                        }
                    case FlagSoulCrystalConfig:
                        return ClientConfig.instantAbsorb;
                    case AddMagneticProjectile:
                        if (args[1] is Projectile magneticProjectile)
                        {
                            if (args[2] is float magnetDamage)
                            {
                                SoAGlobalProjectile.Metalic.Add(magneticProjectile.type, magnetDamage);
                                break;
                            }
                            else
                            {
                                throw new ArgumentException(args[2].GetType().Name + InvalidFloat);
                            }
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + InvalidInt);
                        }
                }
            }
            return false;
        }
    }
}
