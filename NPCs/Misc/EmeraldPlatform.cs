﻿using CollisionLib;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Misc
{
    public class EmeraldPlatform : ModNPC
    {
        public CollisionSurface collider;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 26;
            NPC.lifeMax = 180;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            collider ??= new(NPC.TopLeft, NPC.TopRight, [2, 1, 1, 1], canGrapple: true);
        }

        public override void AI()
        {
            NPC.life--;
            if (NPC.life <= 0)
            {
                SoundEngine.PlaySound(SoundID.Tink, NPC.Center);
                for (int i = 0; i < 25; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald);
                }
            }
            collider.Update();
            collider.endPoints[0] = NPC.Center + (NPC.TopLeft - NPC.Center).RotatedBy(NPC.rotation);
            collider.endPoints[1] = NPC.Center + (NPC.TopRight - NPC.Center).RotatedBy(NPC.rotation);
        }

        public override void PostAI()
        {
            if (collider != null)
            {
                collider.PostUpdate();
            }
        }
    }
}