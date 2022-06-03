using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class BrainSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Brain Of Cthulhu)");
            Tooltip.SetDefault("Spawns 4 Creepers\n" +
                "While Creepers are alive you are invulnerable and cannot attack\n" +
                "Gain a temporary 20% damage boost when all of the creepers die\n" +
                "Creepers take 1 minute to respawn\n" +
                "Cannot be immune to knockback");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().BrainSoul = true;
                if (!player.HasBuff(ModContent.BuffType<CreeperShield>()) && !player.HasItem(ModContent.ItemType<SoulExtractingDagger>()))
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
