using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Consumable;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
    public class MiniAreusStar : ModNPC
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
            NPCID.Sets.CountsAsCritter[Type] = true;
            NPCID.Sets.TownCritter[Type] = true;

            NPC.AddElementElec();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Butterfly);
            NPC.height = 14;
            NPC.width = 14;
            NPC.alpha = 0;
            NPC.aiStyle = NPCAIStyleID.Butterfly;
            NPC.catchItem = ModContent.ItemType<MiniStarCritter>();

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.ElementMultipliers(new[] { 1.0f, 0.5f, 0.8f, 2.0f });
        }

        public override void AI()
        {
            if (NPC.velocity != Vector2.Zero)
            {
                NPC.rotation += MathHelper.ToRadians(5) * NPC.direction;
            }
            base.AI();
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
            float spawnChance = 0f;
            if (spawnInfo.NoInvasionOfAnyKind() && spawnInfo.Player.ZoneNormalSpace && NPC.downedBoss2)
                spawnChance += 1f;
            if (spawnInfo.PlayerInTown)
                spawnChance += 1f;
            return spawnChance;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                ShardsHelpers.DustRing(NPC.Center, 5, ModContent.DustType<AreusDust>());
            }
        }
    }
}