using BattleNetworkElements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Projectiles.NPCProj;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Electrified,
                    BuffID.OnFire,
                    BuffID.Poisoned,
                    BuffID.Confused,
                    ModContent.BuffType<ElectricShock>()
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            Main.npcFrameCount[NPC.type] = 1;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "ShardsOfAtheria/NPCs/AreusMortar_Bestiary"
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPC.AddElec();
        }

        public override void SetDefaults()
        {
            NPC.width = 68;
            NPC.height = 20;
            NPC.damage = 0;
            NPC.defense = 10;
            NPC.lifeMax = 200;
            NPC.lavaImmune = true;
            NPC.value = 2000;
            NPC.knockBackResist = 0;
            NPC.SetElementMultipliersByElement(Element.Elec);
        }

        Player target => Main.player[NPC.target];

        public override void AI()
        {
            NPC.TargetClosest();

            float fireRate = 4;
            if (Main.masterMode)
            {
                fireRate *= 4;
            }
            else if (Main.expertMode)
            {
                fireRate *= 2;
            }
            NPC.ai[0] += 1f;
            if (NPC.ai[0] % 60 / fireRate == 0)
            {
                var position = NPC.Center + new Vector2(0, -20);
                var vector = Vector2.Normalize(target.Center - position);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), position,
                    vector * 16f, ModContent.ProjectileType<AreusGrenadeHostile>(),
                    50, 0, Main.myPlayer);
                SoundEngine.PlaySound(SoundID.Item61, NPC.Center);
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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AreusShard>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GigavoltDelicacy>(), 100));
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> Cannon = ModContent.Request<Texture2D>(Texture + "_Cannon");
            spriteBatch.Draw(Cannon.Value, NPC.position - screenPos, drawColor);
        }
    }
}