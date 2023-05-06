using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class Perish : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<PerishNPC>().perish = true;
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
        public bool perish;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            perish = false;
        }

        public override void OnKill(NPC npc)
        {
            if (perish)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Player player = Main.LocalPlayer;
                    if (Vector2.Distance(player.Center, npc.Center) <= 100)
                    {
                        PlayerDeathReason deathReason = PlayerDeathReason.ByCustomReason(player.name + " perished with " + npc.TypeName + ".");
                        player.KillMe(deathReason, 10000, 0);
                    }
                }
                foreach (NPC npcOther in Main.npc)
                {
                    if (!npcOther.boss || npcOther.active || npcOther.whoAmI != npc.whoAmI)
                    {
                        if (Vector2.Distance(npc.Center, npcOther.Center) <= 100)
                        {
                            NPC.HitInfo info = new()
                            {
                                Damage = npcOther.lifeMax * 3,
                                Crit = true,
                                HideCombatText = true,
                            };
                            npcOther.StrikeNPC(info);
                        }
                    }
                }
            }
        }
    }
}
