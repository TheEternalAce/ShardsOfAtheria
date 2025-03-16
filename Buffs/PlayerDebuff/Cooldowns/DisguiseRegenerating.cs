using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns
{
    public class DisguiseRegenerating : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (ShardsHelpers.AnyBosses())
            {
                player.Shards().disguiseCloakCD = 3600;
                player.buffTime[buffIndex] = 3600;
            }
        }
    }
}
