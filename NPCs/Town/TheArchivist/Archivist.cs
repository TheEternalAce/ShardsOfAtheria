using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Tools.Misc.Slayer;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.Town.TheArchivist
{
    // [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class Archivist : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25;
            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;

            //NPC.Happiness
            //    .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
            //    .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
            //    .SetBiomeAffection<>(AffectionLevel.Hate)
            //    .SetNPCAffection(NPCID.Stylist, AffectionLevel.Love)
            //    .SetNPCAffection(NPCID.Guide, AffectionLevel.Like);

            NPC.AddElementWood();
        }

        internal void SetupShopQuotes(Mod shopQuotes)
        {
            shopQuotes.Call("AddNPC", Mod, Type);
            shopQuotes.Call("SetColor", Type, new Color(255, 255, 255));
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;

            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 19;
            NPC.defense = 10;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersWood);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(key)
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return true;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            if (Main.zenithWorld)
            {
                return true;
            }

            //var stopWatch = new Stopwatch();
            var houseInsideTiles = GetHouseInsideTiles((left + right) / 2, (top + bottom) / 2);
            bool bookshelfFound = BookshelfInHouse(houseInsideTiles);
            return bookshelfFound;
        }

        public static List<Point> GetHouseInsideTiles(int x, int y)
        {
            var addPoints = new List<Point>();
            var checkedPoints = new List<Point>() { new Point(x, y) };
            var offsets = new Point[] { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1), };
            for (int k = 0; k < 1000; k++)
            {
                checkedPoints.AddRange(addPoints);
                addPoints.Clear();
                bool addedAny = false;
                if (checkedPoints.Count > 1000)
                {
                    return checkedPoints;
                }
                for (int l = 0; l < checkedPoints.Count; l++)
                {
                    for (int m = 0; m < offsets.Length; m++)
                    {
                        var newPoint = new Point(checkedPoints[l].X + offsets[m].X, checkedPoints[l].Y + offsets[m].Y);
                        if (WorldGen.InWorld(newPoint.X, newPoint.Y, 10) && !checkedPoints.Contains(newPoint) && !addPoints.Contains(newPoint) &&
                            (!Main.tile[newPoint].HasTile || !Main.tile[newPoint].SolidType() && !Main.tile[newPoint].IsIncludedIn(TileID.Sets.RoomNeeds.CountsAsDoor)) && Main.tile[newPoint].WallType != WallID.None && Main.wallHouse[Main.tile[newPoint].WallType])
                        {
                            addPoints.Add(newPoint);
                            addedAny = true;
                        }
                    }
                }
                if (!addedAny)
                {
                    return checkedPoints;
                }
            }
            return checkedPoints;
        }

        public static bool BookshelfInHouse(List<Point> insideTiles)
        {
            bool shelfFound = false;

            foreach (var p in insideTiles)
            {
                if (Main.tile[p].HasTile)
                {
                    //if (Main.tile[p].IsIncludedIn(TileID.Sets.RoomNeeds.CountsAsTable) ||
                    //    Main.tile[p].IsIncludedIn(TileID.Sets.RoomNeeds.CountsAsChair) ||
                    //    TileID.Sets.Torch[Main.tile[p].TileType])
                    //{
                    //    continue;
                    //}
                    if (Main.tile[p].TileType == TileID.Bookcases)
                    {
                        shelfFound = true;
                    }
                }
            }
            return shelfFound;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() { "Faust" };
        }

        const string DialogueKeyBase = "Mods.ShardsOfAtheria.NPCs.Archivist.Dialogue.";
        public override string GetChat()
        {
            WeightedRandom<string> chat = new();
            Player player = Main.LocalPlayer;
            chat.AddKey(DialogueKeyBase + "Placeholder");
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Archive";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
        }

        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, "Shop")
                .Add(ItemID.Book)
                .Add<Necronomicon>()
                .Add(ItemID.GothicBookcase, Condition.DownedSkeletron)
                .Add(ItemID.BlueDungeonBookcase, Condition.DownedSkeletron)
                .Add(ItemID.GreenDungeonBookcase, Condition.DownedSkeletron)
                .Add(ItemID.PinkDungeonBookcase, Condition.DownedSkeletron)
                .Add(ItemID.ObsidianBookcase, Condition.Hardmode)
                .Add(ItemID.GoldenBookcase, Condition.DownedPirates)
                //.Add<AreusDataDisk>(SoAConditions.AtherianPresent)
                //.Add<AtheriaDataDisk>(SoAConditions.DownedSenterra)
                //.Add<HardlightDataDisk>(SoAConditions.DownedNova)
                //.Add<NovaDataDisk>(SoAConditions.DownedNova, SoAConditions.AtherianPresent)
                //.Add<TerrariansDataDisk>(Condition.DownedMoonLord)
                ;
            npcShop.Register();
        }

        // Make this Town NPC teleport to the King and/or Queen statue when triggered.
        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 19;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.BookStaffShot;
            attackDelay = 20;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}