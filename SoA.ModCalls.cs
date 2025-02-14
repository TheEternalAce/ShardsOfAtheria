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
        public const string FLAG_IS_SLAYER = "checkSlayer";
        public const string FLAG_IS_BOSS_SLAIN = "checkSlainBoss";
        public const string FLAG_IN_COMBAT = "checkCombat";
        public const string FLAG_HAS_SOUL_CYSTAL_ABSORBED = "checkHasSoulCrystal";
        public const string FLAG_SOUL_CRYSTAL_CONFIG = "checkSoulConfig";

        public const string ADD_SOUL_CRYSTAL = "addSoulCrystal";
        public const string GET_PLACEHOLDER_NECRO_ENTRY = "wipNecronomiconEntry";
        public const string ADD_NECRO_ENTRY = "addNecronomiconEntry";
        public const string ADD_COLORED_NECRO_ENTRY = "addColoredNecronomiconEntry";
        public const string ADD_SLAIN_BOSS = "addSlainBoss";
        public const string ADD_TRUE_MELEE_PROJECTILE = "addTrueMeleeProj";
        public const string ADD_MAGNETIC_PROJECTILE = "addMagnetProj";

        public const string INVALID_PLAYER = " is not a valid Player type.";
        public const string INVALID_BOOL = " is not a valid bool.";
        public const string INVALID_INT = " is not a valid int.";
        public const string INVALID_FLOAT = " is not a valid float.";
        public const string INVALID_ITEM = " is not a valid Item type.";
        public const string INVALID_PROJECTILE = " is not a valid Projectile type.";
        public const string INVALID_STRING = " is not a valid string.";
        public const string INVALID_MOD = " is not a valid mod.";
        public const string INVALID_COLOR = " is not a valid color.";
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
                    case FLAG_IS_SLAYER:
                        if (args[1] is Player slayer)
                        {
                            return slayer.Slayer().slayerMode;
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_PLAYER);
                        }
                    case FLAG_IS_BOSS_SLAIN:
                        if (args[1] is int bossType)
                        {
                            return ShardsDownedSystem.slainBosses.Contains(bossType);
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_INT);
                        }
                    case FLAG_IN_COMBAT:
                        if (args[1] is Player combatPlayer)
                        {
                            return combatPlayer.Shards().InCombat;
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_PLAYER);
                        }
                    case ADD_SLAIN_BOSS:
                        if (args[1] is int boss)
                        {
                            ShardsDownedSystem.slainBosses.Add(boss);
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_INT);
                        }
                        break;
                    case ADD_NECRO_ENTRY:
                        if (args[1] is not Mod)
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_MOD);
                        }
                        if (args[2] is not string)
                        {
                            throw new ArgumentException(args[2].GetType().Name + INVALID_STRING);
                        }
                        if (args[3] is not string)
                        {
                            throw new ArgumentException(args[3].GetType().Name + INVALID_STRING);
                        }
                        if (args[4] is not string)
                        {
                            throw new ArgumentException(args[4].GetType().Name + INVALID_STRING);
                        }
                        else
                        {
                            var mod = (Mod)args[1];
                            Entry.NewEntry(mod.Name, (string)args[2], (string)args[3], (string)args[4]);
                        }
                        break;
                    case ADD_COLORED_NECRO_ENTRY:
                        if (args[1] is not Mod)
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_MOD);
                        }
                        if (args[2] is not string)
                        {
                            throw new ArgumentException(args[2].GetType().Name + INVALID_STRING);
                        }
                        if (args[3] is not string)
                        {
                            throw new ArgumentException(args[3].GetType().Name + INVALID_STRING);
                        }
                        if (args[4] is not Color)
                        {
                            throw new ArgumentException(args[4].GetType().Name + INVALID_COLOR);
                        }
                        if (args[5] is not string)
                        {
                            throw new ArgumentException(args[5].GetType().Name + INVALID_STRING);
                        }
                        else
                        {
                            var mod = (Mod)args[1];
                            Entry.NewEntry(mod.Name, (string)args[2], (string)args[3], (Color)args[4], (string)args[5]);
                        }
                        break;
                    case ADD_MAGNETIC_PROJECTILE:
                        if (args[1] is Projectile magneticProjectile)
                        {
                            if (args[2] is float magnetDamage)
                            {
                                if (!SoAGlobalProjectile.Metalic.TryAdd(magneticProjectile.type, magnetDamage))
                                    Logger.Info(magneticProjectile.Name + " is already magnetic.");
                            }
                            else SoAGlobalProjectile.Metalic.Add(magneticProjectile.type, 1f);
                            break;
                        }
                        else throw new ArgumentException(args[1].GetType().Name + INVALID_PROJECTILE);
                    case ADD_SOUL_CRYSTAL:
                        if (args[1] is Player soulsPlayer2)
                        {
                            if (args[2] is string item)
                            {
                                soulsPlayer2.Slayer().soulCrystalNames.Add(item);
                                break;
                            }
                            else
                            {
                                throw new ArgumentException(args[2].GetType().Name + INVALID_STRING);
                            }
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_PLAYER);
                        }
                    case ADD_TRUE_MELEE_PROJECTILE:
                        if (args[1] is Projectile meleeProjectile)
                        {
                            if (SoAGlobalProjectile.TrueMelee.Contains(meleeProjectile.type))
                                Logger.Info(meleeProjectile.Name + " is already true melee.");
                            else SoAGlobalProjectile.TrueMelee.Add(meleeProjectile.type);
                            break;
                        }
                        else throw new ArgumentException(args[1].GetType().Name + INVALID_PROJECTILE);
                    case GET_PLACEHOLDER_NECRO_ENTRY:
                        return Entry.WipEntry();
                    case FLAG_HAS_SOUL_CYSTAL_ABSORBED:
                        if (args[1] is Player soulsPlayer)
                        {
                            if (args[2] is string item)
                            {
                                return soulsPlayer.Slayer().soulCrystalNames.Contains(item);
                            }
                            else
                            {
                                throw new ArgumentException(args[2].GetType().Name + INVALID_INT);
                            }
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + INVALID_PLAYER);
                        }
                    case FLAG_SOUL_CRYSTAL_CONFIG:
                        return ClientConfig.instantAbsorb;
                }
            }
            return false;
        }
    }
}
