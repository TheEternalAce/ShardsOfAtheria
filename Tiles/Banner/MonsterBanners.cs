using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ShardsOfAtheria.NPCs.Variant.Harpy;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ShardsOfAtheria.Tiles.Banner
{
    public class MonsterBanners : ModTile
    {
        public const int ForestHarpyBanner = 0;
        public const int SnowHarpyBanner = 4;
        public const int CaveHarpyBanner = 6;
        public const int DesertHarpyBanner = 1;
        public const int OceanHarpyBanner = 2;
        public const int VoidHarpyBanner = 5;
        public const int CrimsonHarpyBanner = 8;
        public const int CorruptHarpyBanner = 7;
        public const int HallowedHarpyBanner = 3;

        public static List<int> bannerWindHack;

        public override void Load()
        {
            try
            {
                _addSpecialPointSpecialPositions = typeof(TileDrawing).GetField("_specialPositions", BindingFlags.NonPublic | BindingFlags.Instance);
                _addSpecialPointSpecialsCount = typeof(TileDrawing).GetField("_specialsCount", BindingFlags.NonPublic | BindingFlags.Instance);
            }
            catch (Exception e)
            {
                Logging.PublicLogger.Debug(e);
            }

            bannerWindHack = new List<int>();
            IL.Terraria.GameContent.Drawing.TileDrawing.DrawMultiTileVines += TileDrawing_DrawMultiTileVines;
        }
        private static void TileDrawing_DrawMultiTileVines(ILContext il)
        {

            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(MoveType.After,
                i => i.MatchLdloc(9),
                i => i.MatchLdnull(),
                i => i.MatchCall(out _),
                i => i.MatchBrfalse(out _),
                i => i.MatchLdloca(9),
                i => i.MatchCall(out _),
                i => i.MatchBrfalse(out _)
                ))
                return;

            c.Emit(OpCodes.Ldloc, 9);
            c.EmitDelegate((Tile tile) =>
            {
                if (bannerWindHack.Contains(tile.TileType))
                {
                    return 3;
                }
                return 1;
            });
            c.Emit(OpCodes.Stloc, 8);
        }

        public override void Unload()
        {
            bannerWindHack?.Clear();
            bannerWindHack = null;
            _addSpecialPointSpecialPositions = null;
            _addSpecialPointSpecialsCount = null;
        }

        private static FieldInfo _addSpecialPointSpecialPositions;
        private static FieldInfo _addSpecialPointSpecialsCount;

        public static void AddSpecialPoint(TileDrawing renderer, int x, int y, int type)
        {
            if (_addSpecialPointSpecialPositions?.GetValue(renderer) is Point[][] _specialPositions)
            {
                if (_addSpecialPointSpecialsCount?.GetValue(renderer) is int[] _specialsCount)
                {
                    _specialPositions[type][_specialsCount[type]++] = new Point(x, y);
                }
            }
        }

        public static int BannerToItem(int style)
        {
            int npc = BannerToNPC(style);
            if (npc > Main.maxNPCTypes)
            {
                return NPCLoader.GetNPC(npc).BannerItem;
            }
            return 0;
        }

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            DustType = -1;
            TileID.Sets.DisableSmartCursor[Type] = true;
            AddMapEntry(new Color(13, 88, 130), CreateMapEntryName("Banners"));
            bannerWindHack.Add(Type);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int item = BannerToItem(frameX / 18);
            if (item != 0)
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, item);
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                int style = Main.tile[i, j].TileFrameX / 18;
                int npcType = BannerToNPC(style);
                if (npcType != 0)
                {
                    int bannerItem = NPCLoader.GetNPC(npcType).BannerItem;
                    if (ItemID.Sets.BannerStrength.IndexInRange(bannerItem) && ItemID.Sets.BannerStrength[bannerItem].Enabled)
                    {
                        Main.SceneMetrics.NPCBannerBuff[npcType] = true;
                        Main.SceneMetrics.hasBanner = true;
                    }
                }
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.None;
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.tile[i, j].TileFrameX % 18 == 0 && Main.tile[i, j].TileFrameY % 54 == 0)
            {
                AddSpecialPoint(Main.instance.TilesRenderer, i, j, 5);
            }

            return false;
        }

        public static int BannerToNPC(int style)
        {
            switch (style)
            {
                case ForestHarpyBanner:
                    return ModContent.NPCType<ForestHarpy>();
                case SnowHarpyBanner:
                    return ModContent.NPCType<SnowHarpy>();
                case CaveHarpyBanner:
                    return ModContent.NPCType<CaveHarpy>();
                case DesertHarpyBanner:
                    return ModContent.NPCType<DesertHarpy>();
                case OceanHarpyBanner:
                    return ModContent.NPCType<OceanHarpy>();
                case VoidHarpyBanner:
                    return ModContent.NPCType<VoidHarpy>();
                case CrimsonHarpyBanner:
                    return ModContent.NPCType<CrimsonHarpy>();
                case CorruptHarpyBanner:
                    return ModContent.NPCType<CorruptHarpy>();
                case HallowedHarpyBanner:
                    return ModContent.NPCType<HallowedHarpy>();
            }
            return 0;
        }
    }
}