using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Projectiles.NPCProj.Variant;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class VoidHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            List<int> debuffImmunity =
            [
                BuffID.OnFire,
                BuffID.OnFire3,
                BuffID.ShadowFlame,
                BuffID.CursedInferno,
                BuffID.Frostburn,
                BuffID.Frostburn2,
            ];
            NPC.SetImmuneTo(debuffImmunity);

            base.SetStaticDefaults();
            NPC.AddElement(0);
            NPC.AddRedemptionElement(2);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Dark");
            NPC.AddRedemptionElementType("Hot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 24;
            NPC.defense = 14;
            NPC.lifeMax = 100;
            NPC.lavaImmune = true;
            BannerItem = ModContent.ItemType<VoidHarpyBanner>();
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersFire);
        }

        public override void AI()
        {
            base.AI();
            if (NPC.HasBuff(BuffID.OnFire))
            {
                NPC.DelBuff(NPC.FindBuffIndex(BuffID.OnFire));
            }
        }

        public override void SpecialAttack(Vector2 normalizedVelocity)
        {
            if (SoA.Eternity())
            {
                bool validPosition = false;
                var teleportPosition = Vector2.Zero;
                while (!validPosition)
                {
                    teleportPosition = Main.player[NPC.target].Center + Main.rand.NextVector2CircularEdge(200, 200) * (1 - Main.rand.NextFloat(0.33f));
                    if (!Collision.SolidCollision(teleportPosition, NPC.width, NPC.height))
                    {
                        validPosition = true;
                        break;
                    }
                }
                if (validPosition)
                {
                    NPC.Center = teleportPosition;
                    NPC.netUpdate = true;
                    int fireBalls = 4 + Main.rand.Next(0, 7);
                    for (int i = 0; i < fireBalls; i++)
                    {
                        var vector = Vector2.One.RotatedByRandom(MathHelper.TwoPi);
                        vector *= 5f * Main.rand.NextFloat(0.8f, 1.1f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector, ProjectileID.Fireball, 12, 0);
                    }
                }
            }
            int dir = Main.rand.NextBool(2) ? 1 : -1;
            Vector2 position = Main.player[NPC.target].Center + new Vector2(230 * dir, 0);
            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), position, Vector2.Zero,
                ModContent.ProjectileType<FlamePillar>(), 12, 0f, Main.myPlayer);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.PlayerInTown && spawnInfo.Player.ZoneUnderworldHeight)
                return 0.1f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);

            int[,] ores = new int[,]
            {
                { ItemID.Hellstone, 3 },
                { ItemID.Obsidian, 5 },
            };
            npcLoot.Add(ShardsDrops.ManyFromOptions(1, ores));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenAreusMirror>(), 15));
        }
    }
}