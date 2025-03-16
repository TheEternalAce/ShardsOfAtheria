using Microsoft.Xna.Framework;
using ShardsOfAtheria.Gores;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.AreusMachine
{
    public class AreusCrateNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.AddVulnerabilities(2, 8, 9);
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 22;
            NPC.lifeMax = 1;
            NPC.immortal = true;
            NPC.noTileCollide = true;
            NPC.DeathSound = SoundID.Item53;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement(key)
            ]);
        }

        public override void OnSpawn(IEntitySource source)
        {
            NPC.ai[0] = NPC.position.Y;
            NPC.position.Y -= 1000;
        }

        public override void AI()
        {
            NPC.rotation += MathHelper.ToRadians(30);
            if (NPC.position.Y >= NPC.ai[0])
            {
                NPC.immortal = false;
                NPC.StrikeInstantKill();
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.NoInvasionOfAnyKind() && spawnInfo.Player.ZoneForest && Main.dayTime && NPC.downedBoss2 && !spawnInfo.PlayerSafe)
                return .05f;
            return 0f;
        }

        public override void OnKill()
        {
            DestroyCrateVisual();
            SpawnAreusMachine();
        }

        private void DestroyCrateVisual()
        {
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(NPC.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
                if (i < 1)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center,
                        speed, ShardsGores.AreusCratePart.Type);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center,
                        speed, ShardsGores.AreusCratePart2.Type);
                }
            }
        }

        public void SpawnAreusMachine()
        {
            WeightedRandom<int> areusMachine = new();
            areusMachine.Add(ModContent.NPCType<AreusMortar>());
            areusMachine.Add(ModContent.NPCType<ShriekingBeacon>());
            areusMachine.Add(ModContent.NPCType<BiblicallyAccurateAtherian>());
            NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, areusMachine);
        }
    }
}
