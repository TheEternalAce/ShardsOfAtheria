using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
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
    public partial class ShardsHelpers
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
            string keyBase = "Mods.ShardsOfAtheria.NPCs." + npc.ModNPC.Name + ".Dialogue.";
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
                //MonologueUISystem.Instance.ShowMonologue(text);
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

        public static bool AnyBosses()
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.boss) return true;
            }
            return false;
        }

        public static Player GetTargetPlayer(this NPC npc)
        {
            return Main.player[npc.target];
        }

        public static void Track(this NPC npc, Vector2 position, float speed, float inertia)
        {
            // The immediate range around the target (so it doesn't latch onto it when close)
            Vector2 direction = position - npc.Center;
            direction.Normalize();
            direction *= speed;
            npc.velocity = (npc.velocity * (inertia - 1) + direction) / inertia;
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

        /// <summary>
        /// 0 (Fire) <br/>
        /// 1 (Aqua) <br/>
        /// 2 (Elec) <br/>
        /// 3 (Wood)
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="elementID"></param>
        public static void AddElement(this NPC npc, int elementID)
        {
            SoA.TryElementCall("assignElement", npc, elementID);
        }

        /// <summary>
        /// Index 0: Fire <br/>
        /// Index 1: Aqua <br/>
        /// Index 2: Elec <br/>
        /// Index 3: Wood
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="multipliers"></param>
        public static void ElementMultipliers(this NPC npc, float[] multipliers)
        {
            SoA.TryElementCall("assignElement", npc, multipliers);
        }

        /// <summary>
        /// Index 0: Fire <br/>
        /// Index 1: Aqua <br/>
        /// Index 2: Elec <br/>
        /// Index 3: Wood
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="element"></param>
        /// <param name="multiplier"></param>
        public static void ModifyElementMultiplier(this NPC npc, int element, float multiplier)
        {
            SoA.TryElementCall("modifyMultipliers", npc, element, multiplier);
        }

        /// <summary>
        /// 1	(Arcane) <br/>
        /// 2	(Fire) <br/>
        /// 3	(Water) <br/>
        /// 4	(Ice) <br/>
        /// 5	(Earth) <br/>
        /// 6	(Wind) <br/>
        /// 7	(Thunder) <br/>
        /// 8	(Holy) <br/>
        /// 9	(Shadow) <br/>
        /// 10	(Nature) <br/>
        /// 11	(Poison) <br/>
        /// 12	(Blood) <br/>
        /// 13	(Psychic) <br/>
        /// 14	(Celestial) <br/>
        /// 15	(Exposive)
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="elementID"></param>
        public static void AddRedemptionElement(this NPC npc, int elementID)
        {
            SoA.TryRedemptionCall("addElementNPC", elementID, npc.type);
        }

        /// <summary>
        /// Skeleton <br/>
        /// SkeletonHumanoid <br/>
        /// Humanoid <br/>
        /// Undead <br/>
        /// Spirit <br/>
        /// Plantlike <br/>
        /// Demon <br/>
        /// Cold <br/>
        /// Hot <br/>
        /// Wet <br/>
        /// Dragonlike <br/>
        /// Inorganic <br/>
        /// Robotic <br/>
        /// Armed <br/>
        /// Hallowed <br/>
        /// Dark <br/>
        /// Blood <br/>
        /// Slime
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="elementTypeName"></param>
        public static void AddRedemptionElementType(this NPC npc, string elementTypeName)
        {
            SoA.TryRedemptionCall("addNPCToElementTypeList", elementTypeName, npc.type);
        }

        /// <summary>
        /// Cold <br/>
        /// Electricity <br/>
        /// Heat <br/>
        /// Sickness <br/>
        /// Water <br/>
        /// null : neutral, False : Resisted, True : Vulnerable
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="elementTypeName"></param>
        public static void SetDebuffResistance(this NPC npc, string elementTypeName, bool? enabled)
        {
            SoA.TryCalamityCall("SetDebuffVulnerability", npc, elementTypeName, enabled);
        }

        public static readonly float[] NPCMultipliersFire = [0.8f, 2f, 1f, 0.5f];

        public static readonly float[] NPCMultipliersAqua = [0.5f, 0.8f, 2f, 1f];

        public static readonly float[] NPCMultipliersElec = [1f, 0.5f, 0.8f, 2f];

        public static readonly float[] NPCMultipliersWood = [2f, 1f, 0.5f, 0.8f];
    }
}
