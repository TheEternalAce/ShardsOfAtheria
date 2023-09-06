using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.ShardsUI.Monologue;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsNPCHelper
    {
        public static void BasicInWorldGlowmask(this NPC npc, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, Vector2 screenPos, SpriteEffects effects)
        {
            Vector2 drawPos = npc.Center - screenPos;
            spriteBatch.Draw(glowTexture, drawPos, npc.frame, color, npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
        }

        public const int SummonLine = 1;
        public const int MidFightLine = 2;
        public const int DefeatLine = 3;
        public const int ReSummonLine = 4;
        public const int ReMidFightLine = 5;
        public const int ReDefeatLine = 6;
        public const int SlayerSummonLine = 7;
        public const int SlayerMidFightLine = 8;
        public const int DesperationLine = 9;
        public const int LastWords = 10;
        public static string UseBossDialogueWithKey(this NPC npc, string typeName, int index, Color color)
        {
            string keyBase = "Mods.ShardsOfAtheria.NPCs." + typeName + ".Dialogue.";
            string key;
            switch (index)
            {
                default: // Testing
                    key = "Placeholder Text";
                    break;
                case SummonLine:
                    key = keyBase + "InitialSummon";
                    break;
                case MidFightLine:
                    key = keyBase + "MidFight";
                    break;
                case DefeatLine:
                    key = keyBase + "Defeat";
                    break;

                case ReSummonLine:
                    key = keyBase + "InitialSummonRe";
                    break;
                case ReMidFightLine:
                    key = keyBase + "MidFightRe";
                    break;
                case ReDefeatLine:
                    key = keyBase + "DefeatRe";
                    break;

                case SlayerSummonLine:
                    key = keyBase + "InitialSummonSlayer";
                    break;
                case SlayerMidFightLine:
                    key = keyBase + "MidFightSlayer";
                    break;
                case DesperationLine:
                    key = keyBase + "Desperation";
                    break;
                case LastWords:
                    key = keyBase + "Death";
                    break;
            }
            return npc.UseDialogueWithKey(key, color);
        }
        public static string UseDialogueWithKey(this NPC npc, string key, Color color)
        {
            string dialogue = Language.GetTextValue(key);
            npc.UseDialogue(dialogue, color);
            return dialogue;
        }
        public static void UseDialogue(this NPC npc, string text, Color color)
        {
            if (SoA.ClientConfig.dialogue)
            {
                string dialogue = $"<{npc.GivenOrTypeName}>: {text}";
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
                MonologueUISystem.Instance.ShowMonologue(text);
            }
        }

        public static void SlayNPC(this NPC npc, Player player)
        {
            SlayerPlayer slayer = player.Slayer();
            if (slayer.slayerMode)
            {
                ShardsDownedSystem.slainBosses.Add(npc.type);
                Main.ItemDropsDB.GetRulesForNPCID(npc.type, false);
            }
        }

        public static void Track(this NPC npc, Vector2 position, float speed, float inertia)
        {
            // The immediate range around the target (so it doesn't latch onto it when close)
            Vector2 direction = position - npc.Center;
            direction.Normalize();
            direction *= speed;
            npc.velocity = (npc.velocity * (inertia - 1) + direction) / inertia;
        }

        public static void AddBuff<T>(this NPC npc, int time, bool quiet = true) where T : ModBuff
        {
            npc.AddBuff(ModContent.BuffType<T>(), time, quiet);
        }
        public static void ClearBuff<T>(this NPC npc) where T : ModBuff
        {
            npc.DelBuff(ModContent.BuffType<T>());
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

        public static Vector2 GetSpeedStats(this NPC npc)
        {
            var velocityBoost = new Vector2(npc.StatSpeed());
            if (!npc.noGravity)
            {
                velocityBoost.Y = MathHelper.Lerp(1f, velocityBoost.Y, npc.GetGlobalNPC<StatSpeedGlobalNPC>().jumpSpeedInterpolation);
            }
            return velocityBoost;
        }
        public static ref float StatSpeed(this NPC npc)
        {
            return ref npc.GetGlobalNPC<StatSpeedGlobalNPC>().statSpeed;
        }
    }
}
