using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using SagesMania.Items.Placeable;

namespace SagesMania.Tiles
{
	public class CobaltWorkbench : ModTile
	{
		public override void SetDefaults()
		{
			//adjTiles = new int[] { TileID.WorkBenches };
			adjTiles = new int[] { TileID.HeavyWorkBench };
			adjTiles = new int[] { TileID.MythrilAnvil };
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new[] { 18 };
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Cobalt Workbench");
			AddMapEntry(new Color(200, 200, 200), name);
			dustType = DustID.Cobalt;
			disableSmartCursor = true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<CobaltWorkbenchItem>());
		}
	}
}