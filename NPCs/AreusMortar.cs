﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
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
                CustomTexturePath = "ShardsOfAtheria/NPCs/AreusMortar_Bestiary"
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.AddElementFire();
            NPC.AddElementElec();
            NPC.ElementMultipliers(new[] { 0.8f, 1.0f, 0.8f, 2.0f });
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

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
        }

        Player Target => Main.player[NPC.target];
        int active;
        int projWhoAmI = -1;

        public override void AI()
        {
            NPC.TargetClosest();

            if (!Target.dead)
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
                    var vector = Vector2.Normalize(Target.Center - position);
                    int targetDirection = Target.Center.X > NPC.Center.X ? 1 : -1;
                    float distance = Vector2.Distance(position, Target.Center);
                    float offsetAngle = MathHelper.ToRadians(0);
                    vector.RotatedBy(offsetAngle);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), position,
                        vector * 16f, ModContent.ProjectileType<AreusGrenadeHostile>(),
                        15, 0, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item61, NPC.Center);
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement(key)
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            ShardsDrops.AreusCommonDrops(ref npcLoot);
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AreusGrenade>(), 4, 30, 60));
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> Cannon = ModContent.Request<Texture2D>(Texture + "_Cannon");
            var rect = new Rectangle(0, 0, 36, 48);
            var drawPos = NPC.Center - screenPos + new Vector2(0, 24);
            var origin = new Vector2(18, 40);
            float rotation = 0f;
            if (Target != null)
            {
                rotation = (Target.Center - NPC.Center).ToRotation();
                rotation += MathHelper.PiOver2;
            }
            spriteBatch.Draw(Cannon.Value, drawPos, rect, drawColor, rotation, origin, 1f,
                SpriteEffects.None, 0);
        }
    }
}