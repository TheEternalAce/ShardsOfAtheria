using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class Overheat : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Overheat");
            Description.SetDefault("You cannont avtivate Overdrive and are extra vulnerable");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage -= .5f;
            player.statDefense -= 30;
            player.moveSpeed -= .5f;
            player.buffImmune[BuffID.Regeneration] = true;
            player.buffImmune[BuffID.Honey] = true;
            player.buffImmune[BuffID.Campfire] = true;
            player.buffImmune[BuffID.HeartLamp] = true;
            player.buffImmune[ModContent.BuffType<Overdrive>()] = true;
            player.shinyStone = false;
        }
    }
}
