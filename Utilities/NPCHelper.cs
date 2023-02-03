using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Systems;
using Terraria;

namespace ShardsOfAtheria.Utilities
{
    public static class NPCHelper
    {
        public static void BasicInWorldGlowmask(this NPC npc, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, Vector2 screenPos, SpriteEffects effects)
        {
            Vector2 drawOrigin = new Vector2(glowTexture.Width * 0.5f, npc.height * 0.5f);
            Vector2 drawPos = npc.Center - screenPos;
            spriteBatch.Draw(glowTexture, drawPos, npc.frame, color, npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
        }

        public static void SlayBoss(this NPC boss, Player player)
        {
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (slayer.slayerMode)
            {
                ShardsDownedSystem.slainBosses.Add(boss.type);
            }
        }
    }
}
