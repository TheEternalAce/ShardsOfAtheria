using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class Overdrive : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Overdrive: ON");
            Description.SetDefault("Your systems are being pushed beyond their limits");
            Main.buffNoTimeDisplay[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<SMPlayer>().livingMetal)
            {
                player.allDamage *= 2;
                player.moveSpeed += .5f;
                player.armorEffectDrawShadowEOCShield = true;
            }
        }
    }
}
