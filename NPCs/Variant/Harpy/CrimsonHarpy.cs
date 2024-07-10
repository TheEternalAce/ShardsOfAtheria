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
    public class CrimsonHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElement(1);
            NPC.AddElement(3);
            NPC.AddRedemptionElement(12);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Blood");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 22;
            NPC.defense = 15;
            NPC.lifeMax = 80;
            BannerItem = ModContent.ItemType<CrimsonHarpyBanner>();

            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersAqua);

            NPC.SetDebuffResistance("Heat", true);
            NPC.SetDebuffResistance("Cold", false);
            NPC.SetDebuffResistance("Electricity", true);
            NPC.SetDebuffResistance("Sickness", false);

            projectileDamage = 13;
            projectileType = ModContent.ProjectileType<Blood>();
            debuffType = BuffID.Ichor;
        }

        public override void SpecialAttack(Vector2 normalizedVelocity)
        {
            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, normalizedVelocity.RotatedByRandom(MathHelper.ToRadians(15)) * 6f,
                ProjectileID.GoldenShowerHostile, 13, 0f, Main.myPlayer);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ShardsHelpers.NoInvasionOfAnyKind(spawnInfo) && spawnInfo.Player.ZoneCrimson && spawnInfo.Player.ZoneOverworldHeight)
                return 0.1f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            LeadingConditionRule hardmode = new(new Conditions.IsHardmode());
            npcLoot.Add(ItemDropRule.Common(ItemID.ViciousMushroom, 1, 3, 6));
            hardmode.OnSuccess(ItemDropRule.Common(ItemID.Ichor, 5, 1, 3));
            npcLoot.Add(hardmode);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (SoA.Eternity())
            {
                for (int i = 0; i < 10; i++)
                {
                    var vector = Main.rand.NextVector2CircularEdge(4f, 4f);
                    vector *= 1f - Main.rand.NextFloat(0.66f);
                    var projectile = Projectile.NewProjectileDirect(NPC.GetSource_OnHit(NPC), NPC.Center, vector, ProjectileID.BloodNautilusShot, 13, 0);
                    projectile.tileCollide = true;
                }
            }
        }
    }
}