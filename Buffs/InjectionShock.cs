using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class InjectionShock : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Injection Shock");
            Description.SetDefault("You cannont use another injection, cannot regenerate life and mild blood loss");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[BuffID.Regeneration] = true;
            player.buffImmune[BuffID.Honey] = true;
            player.buffImmune[BuffID.Campfire] = true;
            player.buffImmune[BuffID.HeartLamp] = true;
            player.shinyStone = false;
        }
    }
}
