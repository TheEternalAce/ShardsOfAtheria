﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Misc
{
    public class Creeper : ModNPC
    {
        public int aiTimer;

        public override void SetStaticDefaults()
        {
            NPC.AddElementAqua();
            NPC.AddElementWood();

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 26;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.lifeMax = 100;
            NPC.friendly = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersAqua);
        }

        public override void AI()
        {
            Vector2 toOwner = Vector2.Normalize(Main.player[NPC.target].Center - NPC.position);
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                var player = Main.player[NPC.target];
                if (player.dead || !player.active || !player.Slayer().EoCSoul)
                    NPC.active = false;
                else NPC.active = true;

                aiTimer++;
                if (aiTimer <= 1)
                {
                    NPC.velocity = toOwner.RotatedByRandom(MathHelper.ToRadians(5)) * 5f;
                }
                if (aiTimer == 5)
                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
                if (aiTimer == 15)
                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
                if (aiTimer == 25)
                {
                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
                    aiTimer = 0;
                }
                Main.player[NPC.target].AddBuff(ModContent.BuffType<CreeperShield>(), 2);

                // If your minion is flying, you want to do this independently of any conditions
                float overlapVelocity = 0.04f;

                // Fix overlap with other minions
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC other = Main.npc[i];

                    if (i != NPC.whoAmI && other.active && other.type == ModContent.NPCType<Creeper>() && Math.Abs(NPC.position.X - other.position.X) + Math.Abs(NPC.position.Y - other.position.Y) < NPC.width)
                    {
                        if (NPC.position.X < other.position.X)
                        {
                            NPC.velocity.X -= overlapVelocity;
                        }
                        else
                        {
                            NPC.velocity.X += overlapVelocity;
                        }

                        if (NPC.position.Y < other.position.Y)
                        {
                            NPC.velocity.Y -= overlapVelocity;
                        }
                        else
                        {
                            NPC.velocity.Y += overlapVelocity;
                        }
                    }
                }

                if (NPC.Distance(Main.player[NPC.target].Center) > 400f)
                {
                    NPC.Center = Main.player[NPC.target].Center;
                }
                if (NPC.Distance(Main.player[NPC.target].Center) > 200f)
                {
                    NPC.velocity = toOwner * 15f;
                    return;
                }
            }
        }

        public override void OnKill()
        {
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
            Main.player[NPC.target].AddBuff(ModContent.BuffType<CreeperRevenge>(), 600);
            Main.player[NPC.target].ClearBuff(ModContent.BuffType<CreeperShield>());
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 402);
        }
    }
}