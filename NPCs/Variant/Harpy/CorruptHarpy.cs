using Microsoft.Xna.Framework;
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
    public class CorruptHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElement(0);
            NPC.AddElement(3);
            NPC.AddRedemptionElement(9);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Dark");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 18;
            NPC.defense = 12;
            NPC.lifeMax = 75;
            BannerItem = ModContent.ItemType<CorruptHarpyBanner>();
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersFire);

            projectileDamage = 11;
            projectileType = ModContent.ProjectileType<Vile>();
            debuffType = BuffID.Weak;
        }

        public override void SpecialAttack(Vector2 normalizedVelocity)
        {
            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.VileSpit);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.NoInvasionOfAnyKind() && spawnInfo.Player.ZoneCorrupt && spawnInfo.Player.ZoneOverworldHeight)
                return 0.1f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            LeadingConditionRule hardmode = new(new Conditions.IsHardmode());
            npcLoot.Add(ItemDropRule.Common(ItemID.VileMushroom, 1, 3, 6));
            hardmode.OnSuccess(ItemDropRule.Common(ItemID.CursedFlame, 5, 1, 3));
            npcLoot.Add(hardmode);
        }

        public override void OnKill()
        {
            if (SoA.Eternity())
            {
                int maxEaters = Main.rand.Next(2, 4);
                for (int i = 0; i < maxEaters; i++)
                {
                    var randomOffset = Main.rand.NextVector2Circular(20, 20);
                    NPC.NewNPC(NPC.GetSource_Death(), (int)(NPC.Center.X + randomOffset.X), (int)(NPC.Center.Y + randomOffset.Y), NPCID.Corruptor);
                }
            }
        }
    }
}