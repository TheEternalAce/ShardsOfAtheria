using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class ShadowTeleport : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Teleport");
            Description.SetDefault("Press 'Shadow Teleport' to teleport");
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
        }
    }
}
