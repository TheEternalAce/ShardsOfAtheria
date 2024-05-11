using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.NPCProj.Variant;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class OceanHarpy : Harpies
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddElement(1);
            NPC.AddRedemptionElement(3);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Wet");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 17;
            NPC.defense = 11;
            NPC.lifeMax = 60;
            BannerItem = ModContent.ItemType<OceanHarpyBanner>();
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersAqua);

            projectileDamage = 10;
            projectileType = ModContent.ProjectileType<Sea>();
            debuffType = BuffID.Bleeding;
        }

        public override void SpecialAttack(Vector2 normalizedVelocity)
        {
            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, normalizedVelocity * 7f,
                ModContent.ProjectileType<TidalWave>(), 10, 0f, Main.myPlayer);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!ShardsHelpers.NoInvasionOfAnyKind(spawnInfo) && spawnInfo.Player.ZoneBeach)
                return 0.1f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ItemID.Coral, 2, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.Seashell, 2, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.Starfish, 2, 3, 6));
        }
    }
}