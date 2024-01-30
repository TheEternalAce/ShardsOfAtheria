using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class CaveHarpy : Harpies
    {
        static Asset<Texture2D> glowmask;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            }
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPC.AddRedemptionElement(5);
            NPC.AddRedemptionElementType("Humanoid");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 14;
            NPC.defense = 10;
            NPC.lifeMax = 50;
            BannerItem = ModContent.ItemType<CaveHarpyBanner>();
            NPC.ElementMultipliers([2.0f, 0.8f, 0.5f, 1.0f]);

            projectileType = ModContent.ProjectileType<Stone>();
            projectileDamage = 9;
            debuffType = BuffID.Stoned;
        }

        public override bool DebuffCondition => base.DebuffCondition && Main.rand.NextBool(10);

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground);
            bestiaryInfoElements.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns);
            base.SetBestiary(database, bestiaryEntry);
        }

        public override void SpecialAttack(Vector2 velocity)
        {
            base.SpecialAttack(velocity);
        }

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(NPC.Center, Color.Cyan.ToVector3());
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZonePurity && (spawnInfo.Player.ZoneNormalUnderground ||
                spawnInfo.Player.ZoneNormalCaverns))
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            int maxOres = 25;
            int[,] ores = new int[,]
            {
                { ItemID.CopperOre, maxOres },
                { ItemID.TinOre, maxOres },
                { ItemID.IronOre, maxOres },
                { ItemID.LeadOre, maxOres },
                { ItemID.SilverOre, maxOres },
                { ItemID.TungstenOre, maxOres },
                { ItemID.GoldOre, maxOres },
                { ItemID.PlatinumOre, maxOres },
                { ModContent.ItemType<BionicOreItem>(), maxOres }
            };

            npcLoot.Add(new ManyFromOptionsDropRule(1, 1, ores));

            int maxGems = 3;
            int[,] gems = new[,]
            {
                { ItemID.Amethyst, maxGems },
                { ItemID.Diamond, maxGems },
                { ItemID.Emerald, maxGems },
                { ItemID.Ruby, maxGems },
                { ItemID.Sapphire, maxGems },
                { ItemID.Topaz, maxGems },
                { ItemID.Amber, maxGems },
                { ModContent.ItemType<Jade>(), maxGems }
            };
            npcLoot.Add(ShardsDrops.ManyFromOptions(1, gems));
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            NPC.BasicInWorldGlowmask(spriteBatch, glowmask.Value, Color.White, screenPos, effects);
        }
    }
}