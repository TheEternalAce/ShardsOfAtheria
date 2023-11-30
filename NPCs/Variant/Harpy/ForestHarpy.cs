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
    public class ForestHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElementWood();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 10;
            NPC.defense = 6;
            NPC.lifeMax = 40;
            BannerItem = ModContent.ItemType<ForestHarpyBanner>();
            projectileType = ModContent.ProjectileType<Poison>();
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersWood);
            projectileDamage = 5;
        }

        public override void SpecialAttack(Vector2 velocity)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(5);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
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
                spawnInfo.Player.ZoneForest && Main.dayTime)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ItemID.Mushroom, 1, 3, 6));
        }
    }
}