﻿using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class StatSpeedGlobalNPC : GlobalNPC
    {
        public static HashSet<int> IgnoreStatSpeed { get; private set; }

        public float statSpeed;
        public float jumpSpeedInterpolation;

        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        public StatSpeedGlobalNPC()
        {
            statSpeed = 1f;
            jumpSpeedInterpolation = 0.5f;
        }

        public override void Load()
        {
            IgnoreStatSpeed = [];
            On_NPC.UpdateCollision += NPC_UpdateCollision;
        }
        private static void NPC_UpdateCollision(Terraria.On_NPC.orig_UpdateCollision orig, NPC self)
        {
            if (IgnoreStatSpeed.Contains(self.netID))
            {
                orig(self);
                return;
            }

            var velocityBoost = self.GetSpeedStats();
            self.velocity.X *= velocityBoost.X;
            self.velocity.Y *= velocityBoost.Y;
            orig(self);
            self.velocity.X /= velocityBoost.X;
            self.velocity.Y /= velocityBoost.Y;
        }

        public override void SetStaticDefaults()
        {
            //if (Aequus.LogMore)
            //{
            //    Aequus.Instance.Logger.Info("Loading stat speed interactions array...");
            //}
            var val = SoA.GetContentArrayFile("IgnoreStatSpeed");
            foreach (var modDict in val)
            {
                if (modDict.Key == "Vanilla")
                {
                    foreach (var npcName in modDict.Value)
                    {
                        IgnoreStatSpeed.Add(NPCID.Search.GetId(npcName));
                    }
                }
                else if (ModLoader.TryGetMod(modDict.Key, out var mod))
                {
                    //if (Aequus.LogMore)
                    //{
                    //    Aequus.Instance.Logger.Info($"Loading custom wall to item ID table entries for {modDict.Key}...");
                    //}
                    foreach (var npcName in modDict.Value)
                    {
                        if (mod.TryFind<ModNPC>(npcName, out var modNPC))
                        {
                            IgnoreStatSpeed.Add(modNPC.Type);
                        }
                    }
                }
            }
        }

        public override void SetDefaults(NPC npc)
        {
            statSpeed = 1f;
        }

        public override void ResetEffects(NPC npc)
        {
            statSpeed = 1f;
            jumpSpeedInterpolation = 0.5f;
        }

        public override void PostAI(NPC npc)
        {
            if (npc.noTileCollide && statSpeed != 1f && !IgnoreStatSpeed.Contains(npc.netID))
            {
                npc.position += npc.velocity * (statSpeed - 1f);
            }
        }
    }
}