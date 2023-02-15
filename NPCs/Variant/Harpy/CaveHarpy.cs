using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ReLogic.Content;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Items.Placeable;
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
    public class CaveHarpy : ModNPC
    {
        Asset<Texture2D> glowmask;

        public override void Unload()
        {
            glowmask = null;
        }

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
            NPCElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.damage = 14;
            NPC.defense = 10;
            NPC.lifeMax = 50;
            AnimationType = NPCID.Harpy;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<CaveHarpyBanner>();
            NPC.SetElementMultipliersByElement(Element.Metal);

            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Several harpies 300 years ago made their home in the Caverns. Over time, their children have evolved into forms best fit for their new home.")
            });
        }

        public override void AI()
        {
            NPC.ai[0] += 1f;
            if (NPC.ai[0] == 30f || NPC.ai[0] == 60f || NPC.ai[0] == 90f)
            {
                if (Main.rand.NextBool(3))
                {
                    NPC.ai[0] = 91;
                }
                else if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    int num729 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 6f,
                        ModContent.ProjectileType<Stone>(), 9, 0f, Main.myPlayer);
                    Main.projectile[num729].timeLeft = 300;
                }
            }
            else if (NPC.ai[0] >= 400 + Main.rand.Next(400))
            {
                NPC.ai[0] = 0f;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!(spawnInfo.Player.ZoneHallow || spawnInfo.Player.ZoneCrimson || spawnInfo.Player.ZoneCorrupt || spawnInfo.Player.ZoneDungeon || spawnInfo.Player.ZoneSnow
                || spawnInfo.PlayerSafe || spawnInfo.Player.ZoneSnow || spawnInfo.Player.ZoneDesert || spawnInfo.Player.ZoneJungle) && (spawnInfo.Player.ZoneNormalUnderground
                || spawnInfo.Player.ZoneNormalCaverns))
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 2));
            npcLoot.Add(ItemDropRule.OneFromOptions(1, ItemID.CopperOre, ItemID.TinOre, ItemID.IronOre, ItemID.LeadOre, ItemID.SilverOre, ItemID.TungstenOre, ItemID.GoldOre, ItemID.PlatinumOre,
                ModContent.ItemType<BionicOreItem>(), ModContent.ItemType<AreusShard>()));
            npcLoot.Add(ItemDropRule.OneFromOptions(5, ItemID.Amethyst, ItemID.Diamond, ItemID.Emerald, ItemID.Ruby, ItemID.Sapphire, ItemID.Topaz, ItemID.Amber));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if ((Main.expertMode || Main.hardMode) && Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.Stoned, 60);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            string alt = ModContent.GetInstance<ShardsServerConfig>().altCaveHarpy ? "_ALT" : "";
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture + alt);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            NPC.BasicInWorldGlowmask(spriteBatch, texture.Value, drawColor, screenPos, effects);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            NPC.BasicInWorldGlowmask(spriteBatch, glowmask.Value, Color.White, screenPos, effects);
        }
    }
}