using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsNPCHelper
    {
        public static void BasicInWorldGlowmask(this NPC npc, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, Vector2 screenPos, SpriteEffects effects)
        {
            Vector2 drawPos = npc.Center - screenPos;
            spriteBatch.Draw(glowTexture, drawPos, npc.frame, color, npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
        }

        public static void UseDialogue(this NPC npc, string text, Color color)
        {
            string dialogue = $"<{npc.GivenOrTypeName}> {text}";
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (Vector2.Distance(player.Center, npc.Center) <= 2000 || player.whoAmI == npc.target)
                    {
                        ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(dialogue), color, player.whoAmI);
                    }
                }
            }
            else
            {
                Main.NewText(dialogue, color);
            }
        }

        public static void UseDialogueWithKey(this NPC npc, string key, Color color)
        {
            string dialogue = Language.GetTextValue(key);
            npc.UseDialogue(Language.GetTextValue(dialogue), color);
        }

        public static void SlayNPC(this NPC npc, Player player)
        {
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (slayer.slayerMode)
            {
                ShardsDownedSystem.slainBosses.Add(npc.type);
                Main.ItemDropsDB.GetRulesForNPCID(npc.type, false);
            }
        }

        public static void MoveToPoint(this NPC npc, Vector2 point, float speed, bool precise = false)
        {
            if (Vector2.Distance(point, npc.Center) <= speed * 1.5f)
            {
                if (precise)
                {
                    npc.position = point;
                    npc.velocity *= 0;
                }
            }
            else
            {
                npc.position += Vector2.Normalize(point - npc.Center) * speed;
            }
        }

        public static void SetImmuneTo(this NPC npc, List<int> buffTypes)
        {
            int npcType = npc.type;
            for (int i = 0; i < buffTypes.Count; i++)
            {
                int buffType = buffTypes[i];
                NPCID.Sets.SpecificDebuffImmunity[npcType][buffType] = true;
            }
        }

        public static void DropFromItem(int itemType, Player player)
        {
            DropAttemptInfo info = new()
            {
                player = player,
                item = itemType,
                IsExpertMode = Main.expertMode,
                IsMasterMode = Main.masterMode,
                IsInSimulation = false,
                rng = Main.rand
            };
            Main.ItemDropSolver.TryDropping(info);
        }
    }
}
