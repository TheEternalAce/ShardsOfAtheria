using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Placeable.Furniture;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ShardsOfAtheria.Tiles.Furniture
{
    public class AreusNullFieldGenerator : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            DustType = ModContent.DustType<AreusDust>();

            RegisterItemDrop(ModContent.ItemType<NullFieldGenerator>());

            AddMapEntry(new Color(200, 200, 200), CreateMapEntryName());

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = [16, 16];
            TileObjectData.addTile(Type);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.type == ModContent.ProjectileType<AreusNullField>()) projectile.Kill();
            }
            base.KillMultiTile(i, j, frameX, frameY);
        }

        public override bool RightClick(int i, int j)
        {
            if (!ShardsHelpers.AnyProjectile<AreusNullField>())
            {
                Player player = Main.LocalPlayer;
                Tile generator = Main.tile[i, j];
                int frameX = generator.TileFrameX;
                int frameY = generator.TileFrameY;

                Vector2 tilePos = new(i * 16 - frameX, j * 16 - frameY);
                Vector2 offset = new(26, 16);
                Vector2 spawnPos = tilePos + offset;

                Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<AreusNullField>(), 0, 0f, player.whoAmI);
            }
            else
            {
                foreach (Projectile projectile in Main.projectile)
                {
                    if (projectile.type == ModContent.ProjectileType<AreusNullField>() && projectile.ai[0] == 1) projectile.ai[0] = 2;
                }
            }
            return true;
        }
    }
}