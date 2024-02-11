using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
    public class SnowHarpy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = 1f
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.AddElement(1);
            NPC.AddRedemptionElement(4);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Cold");
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.damage = 17;
            NPC.defense = 11;
            NPC.lifeMax = 60;
            AnimationType = NPCID.Harpy;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<SnowHarpyBanner>();
            NPC.ElementMultipliers([2.0f, 0.8f, 2.0f, 1.0f]);
            NPC.coldDamage = true;
        }

        public override void AI()
        {
            if (SoA.Eternity())
            {
                ChillingAura();
            }
            NPC.ai[0] += 1f;
            if (NPC.ai[0] == 30f || NPC.ai[0] == 60f || NPC.ai[0] == 90f)
            {
                if (Main.rand.NextBool(3) && NPC.ai[0] == 30f)
                {
                    Vector2 velocity = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center);
                    Vector2 position = NPC.Center + Vector2.Normalize(velocity) * 10f;
                    Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), position, velocity * 7f,
                        ModContent.ProjectileType<IceShard>(), 10, 0f, Main.myPlayer);
                    proj.friendly = false;
                    proj.hostile = true;
                    proj.tileCollide = true;
                    NPC.ai[0] = 91;
                }
                else if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center,
                        Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 6f,
                        ModContent.ProjectileType<Snow>(), 10, 0f, Main.myPlayer);
                }
            }
            else if (NPC.ai[0] >= 400 + Main.rand.Next(400))
            {
                NPC.ai[0] = 0f;
            }
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
            string key = this.GetLocalizationKey("Bestiary");
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(key)
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!(spawnInfo.Player.ZoneHallow || spawnInfo.Player.ZoneCrimson ||
                spawnInfo.Player.ZoneCorrupt || Main.pumpkinMoon || Main.snowMoon ||
                spawnInfo.Player.ZoneTowerNebula || spawnInfo.Player.ZoneTowerVortex ||
                spawnInfo.Player.ZoneTowerSolar || spawnInfo.Player.ZoneTowerStardust ||
                spawnInfo.PlayerInTown || spawnInfo.Invasion) && spawnInfo.Player.ZoneSnow
                && spawnInfo.Player.ZoneOverworldHeight)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.IceBlock, 2, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.SnowBlock, 2, 3, 6));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode || Main.hardMode)
            {
                target.AddBuff(BuffID.Frostburn, 60);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            NPC.BasicInWorldGlowmask(spriteBatch, texture.Value, drawColor, screenPos, effects);
            return false;
        }
    }
}