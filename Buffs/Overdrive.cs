using Terraria;
using Terraria.ID;
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
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += .5f;
            player.moveSpeed += .5f;
            player.statDefense -= 30;
            player.lifeRegen = 0;
            player.GetModPlayer<SMPlayer>().Overdrive = true;
            player.buffTime[buffIndex] = 18000;
            player.buffImmune[BuffID.Regeneration] = true;
            player.buffImmune[BuffID.Honey] = true;
            player.buffImmune[BuffID.Campfire] = true;
            player.buffImmune[BuffID.HeartLamp] = true;
            player.shinyStone = false;
        }
    }
}
