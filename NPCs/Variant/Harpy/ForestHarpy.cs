using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class ForestHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElement(3);
            NPC.AddRedemptionElement(10);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Plantlike");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 10;
            NPC.defense = 2;
            NPC.lifeMax = 40;
            BannerItem = ModContent.ItemType<ForestHarpyBanner>();
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersWood);

            projectileType = ModContent.ProjectileType<Poison>();
            projectileDamage = 5;
            debuffType = BuffID.Poisoned;
        }

        public override void SpecialAttack(Vector2 normalizedVelocity)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(5);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = normalizedVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, perturbedSpeed * 6f,
                    projectileType, projectileDamage, 0f, Main.myPlayer);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface);
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.NoInvasionOfAnyKind() && !spawnInfo.Player.ZoneSkyHeight &&
                spawnInfo.Player.ZoneForest && !spawnInfo.Player.ZoneJungle && Main.dayTime)
                return 0.1f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ItemID.Mushroom, 1, 3, 6));
            LeadingConditionRule rain = new(new IsInRain());
            rain.OnSuccess(ItemDropRule.Common(ItemID.Worm, 3, 1, 3));
            npcLoot.Add(rain);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (SoA.Eternity())
            {
                for (int i = 0; i < 10; i++)
                {
                    var vector = Main.rand.NextVector2CircularEdge(4f, 4f);
                    vector *= 1f - Main.rand.NextFloat(0.66f);
                    Projectile.NewProjectile(NPC.GetSource_OnHit(NPC), NPC.Center, vector, ProjectileID.JungleSpike, projectileDamage, 0);
                }
            }
        }
    }
}