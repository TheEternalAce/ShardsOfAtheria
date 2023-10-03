using ShardsOfAtheria.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class LifeCycleBuff : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<LifeCycle>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
