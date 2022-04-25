using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.NPCProj;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
    public class Creeper : ModNPC
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

        public override bool PreAI()
        {
            return true;
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
                if (NPC.Distance(Main.player[NPC.target].Center) > 400f)
                {
                    if (Main.player[NPC.target].velocity != Vector2.Zero)
                        NPC.velocity = toOwner * 15f;
                    else NPC.velocity = toOwner * Main.player[NPC.target].velocity * 2f;
                    return;
                }
                aiTimer--;
                if (aiTimer == 25)
                    Projectile.NewProjectileDirect(NPC.GetSpawnSource_ForProjectile(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
                if (aiTimer == 15)
                    Projectile.NewProjectileDirect(NPC.GetSpawnSource_ForProjectile(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
                if (aiTimer == 5)
                    Projectile.NewProjectileDirect(NPC.GetSpawnSource_ForProjectile(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
                if (aiTimer <= 0)
                {
                    NPC.velocity = toOwner.RotatedByRandom(MathHelper.ToRadians(45)) * 6f;
                    aiTimer = 30;
                }
                Main.player[NPC.target].AddBuff(ModContent.BuffType<Buffs.CreeperShield>(), 2);
                
                if (Main.player[NPC.target].dead || !Main.player[NPC.target].active || Main.player[NPC.target].GetModPlayer<SlayerPlayer>().BrainSoul <= 1)
                    NPC.active = false;
                else NPC.active = true;
            }
        }

        public override void OnKill()
        {
            Projectile.NewProjectileDirect(NPC.GetSpawnSource_ForProjectile(), NPC.position, Vector2.Zero, ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, Main.player[NPC.target].whoAmI);
            Main.player[NPC.target].AddBuff(ModContent.BuffType<Buffs.CreeperRevenge>(), 600);
            Main.player[NPC.target].ClearBuff(ModContent.BuffType<Buffs.CreeperShield>());
            Gore.NewGore(NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 402);
        }
    }
}