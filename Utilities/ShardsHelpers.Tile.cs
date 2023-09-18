using System.Runtime.CompilerServices;
using Terraria;

namespace ShardsOfAtheria.Utilities
{
    public partial class ShardsHelpers
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSolid(this Tile tile)
        {
            return tile.HasTile && SolidType(tile) && !tile.IsActuated;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSolid(int i, int j)
        {
            return IsSolid(Main.tile[i, j]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SolidType(this Tile tile)
        {
            return Main.tileSolid[tile.TileType];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SolidType(int i, int j)
        {
            return SolidType(Main.tile[i, j]);
        }

        public static bool IsIncludedIn(this Tile tile, int[] arr)
        {
            return arr.ContainsAny(tile.TileType);
        }
    }
}
