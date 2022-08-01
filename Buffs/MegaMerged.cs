using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class Megamerged : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Megamerged");
            Description.SetDefault("'BIOLINK ESTABLISHED! M.E.G.A. SYSTEM ONLINE!'");
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
        }
    }
}
