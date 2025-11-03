using Microsoft.Xna.Framework;
using System;
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
            for (var i = 0; i < 20; i++)
            {
                Vector2 spawnPos = npc.Center + Main.rand.NextVector2CircularEdge(100, 100);
                Vector2 offset = spawnPos - Main.LocalPlayer.Center;
                if (Math.Abs(offset.X) > Main.screenWidth * 0.6f || Math.Abs(offset.Y) > Main.screenHeight * 0.6f) //dont spawn dust if its pointless
                    continue;
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Stone, 0, 0, 100, Color.DarkGray);
                dust.velocity = npc.velocity;
                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(npc.Center - dust.position) * Main.rand.NextFloat(5f);
                    dust.position += dust.velocity * 5f;
                }
                dust.noGravity = true;
            }
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
