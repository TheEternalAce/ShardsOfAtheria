using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.AreusMachine
{
    public class ShriekingBeacon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // Specify the debuffs it is immune to
            List<int> buffImmunities =
            [
                BuffID.Electrified,
                BuffID.OnFire,
                BuffID.Poisoned,
                BuffID.Venom,
                BuffID.Confused,
                ModContent.BuffType<ElectricShock>(),
            ];
            NPC.SetImmuneTo(buffImmunities);

            Main.npcFrameCount[NPC.type] = 1;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                CustomTexturePath = Texture + "_Bestiary"
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.AddElement(0);
            NPC.AddElement(2);

            NPC.AddDamageType(5, 12);
            NPC.AddVulnerabilities(2, 8, 9);

            NPC.AddRedemptionElement(7);

            NPC.AddRedemptionElementType("Inorganic");
            NPC.AddRedemptionElementType("Robotic");
        }

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 32;
            NPC.damage = 0;
            NPC.defense = 10;
            NPC.lifeMax = 200;
            NPC.lavaImmune = true;
            NPC.value = 2000;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 22;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;

            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersElec);

            NPC.SetDebuffResistance("Sickness", false);
            NPC.SetDebuffResistance("Electricity", false);
            NPC.SetDebuffResistance("Heat", false);
            NPC.SetDebuffResistance("Cold", false);
            NPC.SetDebuffResistance("Water", true);
        }

        Player Target => Main.player[NPC.target];
        int shootTimer = 0;
        bool shriek = false;

        public override void AI()
        {
            NPC.TargetClosest();

            UpdateVisuals();
            if (Target == null || Target.dead) return;
            shootTimer++;
            if (shootTimer == 1)
            {
                state = OPEN;
                shriek = !Main.rand.NextBool(3);
            }
            else if (shriek && shootTimer == 15) state = SHRIEK;
            if (shriek)
            {
                if (shootTimer == 20) SoundEngine.PlaySound(SoundID.ScaryScream, NPC.Center);
                else if (shootTimer <= 120 && shootTimer >= 20 && shootTimer % 20 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero,
                        ModContent.ProjectileType<AreusLeadShriek>(), 12, 0f, Main.myPlayer);
                else if (shootTimer == 121) state = END_SHRIEK;
                else if (shootTimer == 150) state = CLOSE;
            }
            else
            {
                if (Collision.CanHit(NPC.position, 10, 10, Target.position, 10, 10) && (shootTimer == 20 || shootTimer == 30 || shootTimer == 40))
                {
                    var projectilePosition = NPC.Center + new Vector2(0, 2);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), projectilePosition,
                        Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 16f,
                        ModContent.ProjectileType<BeaconLaser>(), 9, 0f, Main.myPlayer);
                }
                if (shootTimer == 50) state = CLOSE;
            }
            if (shootTimer >= 300 + Main.rand.Next(100)) shootTimer = 0;
        }

        int state = IDLE;
        const int IDLE = -1;
        const int OPEN = 0;
        const int SHRIEK = 1;
        const int CLOSE = 2;
        const int END_SHRIEK = 3;
        private void UpdateVisuals()
        {
            int stopFrame = 0;
            int frameAdd = 1;
            switch (state)
            {
                case OPEN:
                    stopFrame = 2;
                    break;
                case SHRIEK:
                    stopFrame = 5;
                    break;
                case CLOSE:
                    frameAdd = -1;
                    stopFrame = 0;
                    break;
                case END_SHRIEK:
                    frameAdd = -1;
                    stopFrame = 2;
                    break;
            }
            if (state != IDLE && ++NPC.frameCounter == 3)
            {
                NPC.frameCounter = 0;
                frame += frameAdd;
                if (frame == stopFrame) state = IDLE;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement(key)
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            ShardsDrops.AreusCommonDrops(ref npcLoot);
            npcLoot.Add(ItemDropRule.Common(ItemID.LeadBar, 2, 2, 5));
        }

        int frame = 0;
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            Asset<Texture2D> head = ModContent.Request<Texture2D>(Texture + "_Head");
            var rect = new Rectangle(0, 28 * frame, 24, 28);
            Vector2 offset = new(0, 4);
            var drawPos = NPC.Center - screenPos + offset;
            var origin = new Vector2(11, 22);
            float rotation = 0f;
            spriteBatch.Draw(head.Value, drawPos, rect, drawColor, rotation, origin, 1f, SpriteEffects.None, 0);
        }
    }
}