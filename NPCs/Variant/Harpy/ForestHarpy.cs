using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class ForestHarpy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                               // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                               // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.damage = 10;
            NPC.defense = 6;
            NPC.lifeMax = 40;
            AnimationType = NPCID.Harpy;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<ForestHarpyBanner>();
        }

        public override void AI()
        {
            NPC.ai[0] += 1f;
            if (NPC.ai[0] == 30f || NPC.ai[0] == 60f || NPC.ai[0] == 90f)
            {
                if (Main.rand.NextBool(3))
                {
                    Vector2 velocity = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center);
                    float numberProjectiles = 3;
                    float rotation = MathHelper.ToRadians(5);
                    Vector2 position = NPC.Center + Vector2.Normalize(velocity) * 10f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                        Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), position, perturbedSpeed * 6f,
                            ModContent.ProjectileType<Poison>(), 7, 0f, Main.myPlayer);
                        proj.DamageType = DamageClass.Ranged;
                    }
                    NPC.ai[0] = 91;
                }
                else if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    int num729 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 6f,
                        ModContent.ProjectileType<Poison>(), 7, 0f, Main.myPlayer);
                    Main.projectile[num729].timeLeft = 300;
                }
            }
            else if (NPC.ai[0] >= 400 + Main.rand.Next(400))
            {
                NPC.ai[0] = 0f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Several harpies 300 years ago made their homes in the Forests and Jungles. Over time, their children have evolved into forms best fit for their new home.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!(spawnInfo.Player.ZoneHallow || spawnInfo.Player.ZoneCrimson || spawnInfo.Player.ZoneCorrupt || Main.eclipse || spawnInfo.Player.ZoneTowerNebula || spawnInfo.Player.ZoneTowerVortex
                || spawnInfo.Player.ZoneTowerSolar || spawnInfo.Player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || spawnInfo.PlayerSafe || spawnInfo.Player.ZoneSnow
                || spawnInfo.Player.ZoneDesert) && spawnInfo.Player.ZoneOverworldHeight && Main.dayTime)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Mushroom, 1, 3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60);
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