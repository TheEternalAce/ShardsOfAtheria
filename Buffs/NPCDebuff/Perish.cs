using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class Perish : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust[] ring = ShardsHelpers.DustAura(npc.Center, 100, DustID.Stone, 20, npc.velocity);
            foreach (Dust d in ring) d.color = Color.DarkGray;
        }
    }

    public class PerishNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.HasBuff<Perish>())
            {
                foreach (Player player in Main.player)
                {
                    if (player != null)
                    {
                        if (!player.immune)
                        {
                            if (Vector2.Distance(player.Center, npc.Center) <= 100)
                            {
                                PlayerDeathReason deathReason = PlayerDeathReason.ByCustomReason(NetworkText.FromKey("ShardsOfAtheria.DeathMessages.PerishSong", player.name, npc.TypeName));
                                player.KillMe(deathReason, player.statLifeMax2 * 6, 0);
                            }
                        }
                    }
                }
                foreach (NPC npcOther in Main.npc)
                {
                    if (!npcOther.boss && npcOther.whoAmI != npc.whoAmI && npcOther.CanBeChasedBy())
                    {
                        if (Vector2.Distance(npc.Center, npcOther.Center) <= 100)
                        {
                            npcOther.StrikeInstantKill();
                        }
                    }
                }
            }
        }
    }
}
