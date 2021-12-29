using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
namespace ShardsOfAtheria
{
    public class Config
    {
        public static bool MegamergeVisual = true;
        public static bool NoRocketFlightToggle = true;
        public static bool sapphireMinion = true;
        public static bool areusWeaponsCostMana = false;

        //The file will be stored in "Terraria/ModLoader/Mod Configs/AcesMania.json"
        static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "AcesMania.json");

        static Preferences Configuration = new Preferences(ConfigPath);

        public static void Load()
        {
            //Reading the config file
            bool success = ReadConfig();

            if (!success)
            {
                CreateConfig();
            }
        }

        //Returns "true" if the config file was found and successfully loaded.
        static bool ReadConfig()
        {
            if (Configuration.Load())
            {
                Configuration.Get("MegamergeVisual", ref MegamergeVisual);
                Configuration.Get("NoRocketFlightToggle", ref NoRocketFlightToggle);
                Configuration.Get("sapphireMinion", ref sapphireMinion);
                Configuration.Get("areusWeaponsCostMana", ref areusWeaponsCostMana);
                return true;
            }
            return false;
        }

        //Creates a config file. This will only be called if the config file doesn't exist yet or it's invalid. 
        static void CreateConfig()
        {
            Configuration.Clear();
            Configuration.Put("MegamergeVisual", MegamergeVisual);
            Configuration.Put("NoRocketFlightToggle", NoRocketFlightToggle);
            Configuration.Put("sapphireMinion", sapphireMinion);
            Configuration.Put("areusWeaponsCostMana", areusWeaponsCostMana);
            Configuration.Save();
        }
    }
}
