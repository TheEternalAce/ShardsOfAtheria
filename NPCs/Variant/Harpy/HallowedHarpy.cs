using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class HallowedHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElement(0);
            NPC.AddElement(1);
            NPC.AddElement(2);
            NPC.AddElement(3);
            NPC.AddRedemptionElement(8);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Hallowed");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 52;
            NPC.defense = 20;
            NPC.lifeMax = 150;
            BannerItem = ModContent.ItemType<HallowedHarpyBanner>();
            NPC.ElementMultipliers([2.0f, 1.5f, 0.5f, 1.0f]);

            projectileDamage = 28;
            projectileType = ModContent.ProjectileType<Crystal>();
            debuffType = BuffID.Confused;
        }

        public override void AI()
        {
            base.AI();
            if (SoA.Eternity())
            {
                SmiteAura();
            }
        }

        public override void SpecialAttack(Vector2 normalizedVelocity)
        {
            Vector2 position = NPC.Center + Vector2.Normalize(normalizedVelocity) * 10f;
            Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), position, normalizedVelocity * 10f,
                ProjectileID.QueenSlimeMinionBlueSpike, 10, 0f, Main.myPlayer);
            proj.friendly = false;
            proj.hostile = true;
        }

        const int SMITING_RADIUS = 250;
        void SmiteAura()
        {
            for (var i = 0; i < 20; i++)
            {
                Vector2 spawnPos = NPC.Center + Main.rand.NextVector2CircularEdge(SMITING_RADIUS, SMITING_RADIUS);
                Vector2 offset = spawnPos - Main.LocalPlayer.Center;
                if (Math.Abs(offset.X) > Main.screenWidth * 0.6f || Math.Abs(offset.Y) > Main.screenHeight * 0.6f) //dont spawn dust if its pointless
                    continue;
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.HallowedWeapons, 0, 0, 100);
                dust.velocity = NPC.velocity;
                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(NPC.Center - dust.position) * Main.rand.NextFloat(5f);
                    dust.position += dust.velocity * 5f;
                }
                dust.noGravity = true;
            }
            Smiting();
        }

        void Smiting()
        {
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead)
                {
                    var distToPlayer = Vector2.Distance(player.Center, NPC.Center);
                    if (distToPlayer <= SMITING_RADIUS)
                    {
                        player.AddBuff(ModContent.Find<ModBuff>("FargowiltasSouls", "SmiteBuff").Type, 600);
                    }
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!ShardsHelpers.NoInvasionOfAnyKind(spawnInfo) && spawnInfo.Player.ZoneHallow &&
                spawnInfo.Player.ZoneOverworldHeight)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ItemID.CrystalShard, 3, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.PixieDust, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.UnicornHorn, 3, 1, 3));
        }
    }
}