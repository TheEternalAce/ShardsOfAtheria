using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Tools.Misc;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class BrainSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Spawns 4 Creepers\n" +
                "While Creepers are alive you are invulnerable and cannot attack\n" +
                "Gain a temporary 20% damage boost when all of the creepers die\n" +
                "Creepers take 1 minute to respawn\n" +
                "Cannot be immune to knockback";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ShardsConfigClientSide>().instantAbsorb)
            {
                if (!player.HasBuff(ModContent.BuffType<CreeperShield>()))
                {
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X + 20), (int)(player.Center.Y + 20), ModContent.NPCType<Creeper>());
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X - 20), (int)(player.Center.Y + 20), ModContent.NPCType<Creeper>());
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X + 20), (int)(player.Center.Y - 20), ModContent.NPCType<Creeper>());
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X - 20), (int)(player.Center.Y - 20), ModContent.NPCType<Creeper>());
                }
            }
            return base.UseItem(player);
        }
    }
}
