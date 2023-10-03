using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
    public class AreusMortar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // Specify the debuffs it is immune to
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Electrified] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<ElectricShock>()] = true;

            Main.npcFrameCount[NPC.type] = 1;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                CustomTexturePath = "ShardsOfAtheria/NPCs/AreusMortar_Bestiary"
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 68;
            NPC.height = 80;
            NPC.damage = 0;
            NPC.defense = 10;
            NPC.lifeMax = 200;
            NPC.lavaImmune = true;
            NPC.value = 2000;
            NPC.knockBackResist = 0;
            NPC.alpha = 255;

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
        }

        Player target => Main.player[NPC.target];
        int active;
        int projWhoAmI = -1;

        public override void AI()
        {
            if (active == 0)
            {
                projWhoAmI = Projectile.NewProjectile(NPC.GetSource_FromThis(),
                    NPC.Center - new Vector2(0, 1000), Vector2.Zero,
                    ModContent.ProjectileType<AreusCrateEnemy>(), 0, 0, Main.myPlayer);
                active = 1;
            }
            else if (active == 1)
            {
                Projectile proj = Main.projectile[projWhoAmI];
                if (proj.Hitbox.Intersects(NPC.Hitbox))
                {
                    proj.Kill();
                    SoundEngine.PlaySound(SoundID.Item53, NPC.Center);
                    for (var i = 0; i < 28; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                        Dust d = Dust.NewDustPerfect(NPC.Center, DustID.Electric, speed * 2.4f);
                        d.fadeIn = 1.3f;
                        d.noGravity = true;
                    }
                    active = 2;
                    NPC.alpha = 0;
                }
            }
            else if (active == 2)
            {
                NPC.TargetClosest();

                if (!target.dead)
                {
                    float fireRate = 0.5f;
                    if (Main.masterMode)
                    {
                        fireRate = 1;
                    }
                    else if (Main.expertMode)
                    {
                        fireRate = 0.75f;
                    }
                    NPC.ai[0] += 1f;
                    if (NPC.ai[0] % (60 / fireRate) == 0)
                    {
                        var position = NPC.Center + new Vector2(0, 16);
                        var vector = Vector2.Normalize(target.Center - position);
                        int targetDirection = target.Center.X > NPC.Center.X ? 1 : -1;
                        float distance = Vector2.Distance(position, target.Center);
                        float offsetAngle = MathHelper.ToRadians(0);
                        vector.RotatedBy(offsetAngle);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), position,
                            vector * 16f, ModContent.ProjectileType<AreusGrenadeHostile>(),
                            15, 0, Main.myPlayer);
                        SoundEngine.PlaySound(SoundID.Item61, NPC.Center);
                    }
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!(Main.eclipse || spawnInfo.Player.ZoneTowerNebula ||
                spawnInfo.Player.ZoneTowerVortex || spawnInfo.Player.ZoneTowerSolar ||
                spawnInfo.Player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon ||
                spawnInfo.PlayerInTown || spawnInfo.Player.ZoneSnow || spawnInfo.Invasion) &&
                spawnInfo.Player.ZoneForest && Main.dayTime)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            ShardsDrops.AreusCommonDrops(ref npcLoot);
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AreusGrenade>(), 4, 1, 30));
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (active == 2)
            {
                Asset<Texture2D> Cannon = ModContent.Request<Texture2D>(Texture + "_Cannon");
                var rect = new Rectangle(0, 0, 36, 48);
                var drawPos = NPC.Center - screenPos + new Vector2(0, 24);
                var origin = new Vector2(18, 40);
                float rotation = 0f;
                if (target != null)
                {
                    rotation = (target.Center - NPC.Center).ToRotation();
                    rotation += MathHelper.ToRadians(90);
                }
                spriteBatch.Draw(Cannon.Value, drawPos, rect, drawColor, rotation, origin, 1f,
                    SpriteEffects.None, 0);
            }
        }
    }
}