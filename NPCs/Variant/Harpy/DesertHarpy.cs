using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.NPCProj.Variant;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class DesertHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElement(2);
            NPC.AddRedemptionElement(7);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Hot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 20;
            NPC.defense = 12;
            NPC.lifeMax = 75;
            BannerItem = ModContent.ItemType<DesertHarpyBanner>();
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersElec);

            projectileDamage = 12;
            projectileType = ModContent.ProjectileType<Static>();
            debuffType = ModContent.BuffType<ElectricShock>();
        }

        public override void SpecialAttack(Vector2 normalizedVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item1);
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(5);
            Vector2 position = NPC.Center + Vector2.Normalize(normalizedVelocity) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = normalizedVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(NPC.GetSource_FromThis(), position, perturbedSpeed * 12f,
                    ModContent.ProjectileType<CactusNeedle>(), 12, 0f, Main.myPlayer);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!ShardsHelpers.NoInvasionOfAnyKind(spawnInfo) && spawnInfo.Player.ZoneOverworldHeight && spawnInfo.Player.ZoneDesert)
                return 0.1f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ItemID.Cactus, 3, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 3, 3, 6));
        }
    }
}