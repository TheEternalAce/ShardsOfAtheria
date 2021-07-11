using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using SagesMania.Items.Placeable;

namespace SagesMania.Tiles
{
	public class AreusForge : ModTile
	{
		public override void SetDefaults()
		{
			adjTiles = new int[] { TileID.AdamantiteForge };
			adjTiles = new int[] { TileID.Furnaces };
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Areus Forge");
			AddMapEntry(new Color(200, 200, 200), name);
			dustType = DustID.Electric;
		}

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			r = 0;
			b = 1;
			g = 1;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			// Above code works, but since we are just mimicking another tile, we can just use the same value.
			frame = Main.tileFrame[TileID.Furnaces];
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<AreusForgeItem>());
		}
	}
}