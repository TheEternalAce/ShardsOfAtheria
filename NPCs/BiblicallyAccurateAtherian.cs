using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Gores;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
    public class BiblicallyAccurateAtherian : ModNPC
    {
        static Asset<Texture2D> ringTexture;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                ringTexture = ModContent.Request<Texture2D>(Texture + "_Ring");
            }
        }

        public override void Unload()
        {
            ringTexture = null;
        }

        public override void SetStaticDefaults()
        {
            // Specify the debuffs it is immune to
            List<int> buffImmunities = new()
            {
                BuffID.Electrified,
                BuffID.OnFire,
                BuffID.Poisoned,
                BuffID.Confused,
                ModContent.BuffType<ElectricShock>(),
            };
            NPC.SetImmuneTo(buffImmunities);

            Main.npcFrameCount[NPC.type] = 1;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                CustomTexturePath = "ShardsOfAtheria/NPCs/BiblicallyAccurateAtherian_Bestiary"
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.AddElementElec();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Ghost);
            NPC.alpha = 0;
            NPC.aiStyle = 14;
            NPC.noTileCollide = false;

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.ElementMultipliers(new[] { 1.0f, 0.5f, 0.8f, 2.0f });
        }

        int shootTimer = 0;
        public override void AI()
        {
            shootTimer++;
            if (shootTimer == 20 || shootTimer == 40 || shootTimer == 60)
            {
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    int num729 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center,
                        Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 12f,
                        ProjectileID.MartianTurretBolt, 9, 0f, Main.myPlayer);
                    Main.projectile[num729].timeLeft = 300;
                }
            }
            else if (shootTimer >= 200 && Main.rand.NextBool(5))
            {
                for (var i = 0; i < 30; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(NPC.Center, DustID.Electric, speed * 6f);
                    d.fadeIn = 1.3f;
                    d.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.NPCHit53, NPC.Center);
                float MaxDistance = 150;
                foreach (NPC npc in Main.npc)
                {
                    if (NPC.Distance(npc.Center) < MaxDistance)
                    {
                        if (npc.CanBeChasedBy() && npc.whoAmI != NPC.whoAmI && npc.type != Type)
                        {
                            NPC.HitInfo info = new()
                            {
                                Damage = 60
                            };
                            npc.StrikeNPC(info);
                        }
                    }
                }
                foreach (Player player in Main.player)
                {
                    if (NPC.Distance(player.Center) < MaxDistance)
                    {
                        if (player.immuneTime == 0 && !player.creativeGodMode)
                        {
                            Player.HurtInfo info = new()
                            {
                                Damage = 60,
                                DamageSource = PlayerDeathReason.ByNPC(NPC.whoAmI)
                            };
                            player.Hurt(info);
                        }
                    }
                }
            }
            else if (shootTimer >= 400 + Main.rand.Next(200))
            {
                shootTimer = 0;
            }
            else if (shootTimer >= 200)
            {
                shootTimer = 0;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(this.GetLocalizationKey("Bestiary"))
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.NoInvasionOfAnyKind() && spawnInfo.Player.ZoneSkyHeight && NPC.downedBoss2)
                return .05f;
            return 0f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    var velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(0.8f, 2f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, velocity, ShardsGores.AreusConstructRod.Type);
                }
                for (int i = 0; i < 3; i++)
                {
                    var velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(0.8f, 2f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, velocity, ShardsGores.AreusConstructEye.Type + i);
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            ShardsDrops.AreusCommonDrops(ref npcLoot);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(
                ringTexture.Value,
                NPC.Center - ringTexture.Size() * 0.5f - screenPos,
                drawColor);

            var texture = TextureAssets.Npc[Type];
            spriteBatch.Draw(
                texture.Value,
                NPC.Center - texture.Size() * 0.5f - screenPos + new Vector2(0, -18),
                drawColor);

            return false;
        }
    }
}