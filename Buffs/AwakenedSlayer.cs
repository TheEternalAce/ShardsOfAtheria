using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class AwakenedSlayer : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awakened Slayer");
            Description.SetDefault("'You are not you anymore. I am in control.'");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.hairColor = new Color(255, 0, 218);
        }
    }
}
