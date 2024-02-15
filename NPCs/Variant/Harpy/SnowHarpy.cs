using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.Melee.GenesisRagnarok.IceStuff;
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
    public class SnowHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElement(1);
            NPC.AddRedemptionElement(4);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Cold");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 17;
            NPC.defense = 11;
            NPC.lifeMax = 60;
            BannerItem = ModContent.ItemType<SnowHarpyBanner>();
            NPC.ElementMultipliers([2.0f, 0.8f, 2.0f, 1.0f]);
            NPC.coldDamage = true;

            projectileDamage = 10;
            projectileType = ModContent.ProjectileType<Snow>();
            debuffType = BuffID.Chilled;
        }

        public override void AI()
        {
            base.AI();
            if (SoA.Eternity())
            {
                ChillingAura();
            }
        }

        public override void SpecialAttack(Vector2 velocity)
        {
            Vector2 position = NPC.Center + Vector2.Normalize(velocity) * 10f;
            Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), position, velocity * 7f,
                ModContent.ProjectileType<IceShard>(), 10, 0f, Main.myPlayer);
            proj.friendly = false;
            proj.hostile = true;
            proj.tileCollide = true;
        }

        const int CHILLING_RADIUS = 250;
        void ChillingAura()
        {
            for (var i = 0; i < 20; i++)
            {
                Vector2 spawnPos = NPC.Center + Main.rand.NextVector2CircularEdge(CHILLING_RADIUS, CHILLING_RADIUS);
                Vector2 offset = spawnPos - Main.LocalPlayer.Center;
                if (Math.Abs(offset.X) > Main.screenWidth * 0.6f || Math.Abs(offset.Y) > Main.screenHeight * 0.6f) //dont spawn dust if its pointless
                    continue;
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Snow, 0, 0, 100);
                dust.velocity = NPC.velocity;
                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(NPC.Center - dust.position) * Main.rand.NextFloat(5f);
                    dust.position += dust.velocity * 5f;
                }
                dust.noGravity = true;
            }
            Chilling();
        }

        void Chilling()
        {
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead)
                {
                    var distToPlayer = Vector2.Distance(player.Center, NPC.Center);
                    if (distToPlayer <= CHILLING_RADIUS)
                    {
                        player.AddBuff(BuffID.Chilled, 600);
                    }
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!ShardsHelpers.NoInvasionOfAnyKind(spawnInfo) && spawnInfo.Player.ZoneSnow && spawnInfo.Player.ZoneOverworldHeight)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ItemID.IceBlock, 2, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.SnowBlock, 2, 3, 6));
        }
    }
}