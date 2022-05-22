using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.NPCProj;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
    public class TrueCreeper : ModNPC
    {
        public int aiTimer;

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
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = NPC.lifeMax;
            NPC.damage = NPC.damage;
        }

        public override void AI()
        {
            Vector2 toOwner = Vector2.Normalize(Main.player[NPC.target].Center - NPC.position);
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active || NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.player[NPC.target].dead || !Main.player[NPC.target].active || !Main.player[NPC.target].GetModPlayer<SynergyPlayer>().brainLordSynergy)
                    NPC.active = false;
                else NPC.active = true;

                aiTimer++;
                if (aiTimer <= 1)
                {
                    NPC.velocity = toOwner.RotatedByRandom(MathHelper.ToRadians(5)) * 5f;
                }
                if (aiTimer == 5)
                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 60, 0f, Main.player[NPC.target].whoAmI);
                if (aiTimer == 15)
                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 60, 0f, Main.player[NPC.target].whoAmI);
                if (aiTimer == 25)
                {
                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 60, 0f, Main.player[NPC.target].whoAmI);
                    aiTimer = 0;
                }
                Main.player[NPC.target].AddBuff(ModContent.BuffType<TrueCreeperShield>(), 2);

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
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 402);
        }
    }
}