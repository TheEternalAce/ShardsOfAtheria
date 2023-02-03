using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DataDisks
{
    public abstract class DataDisk : ModItem
    {
        public int diskType = 0;
        public bool readingDisk = false;

        Asset<Texture2D> disk = ModContent.Request<Texture2D>("ShardsOfAtheria/Items/DataDisks/DataDisk");

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.maxStack = 1;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.MenuTick;
            Item.noUseGraphic = true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(disk.Value, position, null, drawColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            return !readingDisk;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Item.BasicInWorldGlowmask(spriteBatch, disk.Value, lightColor, rotation, scale);
            return !readingDisk;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Asset<Texture2D> diskType = ModContent.Request<Texture2D>(Texture);
            if (!readingDisk)
            {
                Item.BasicInWorldGlowmask(spriteBatch, diskType.Value, Color.White, rotation, scale);
            }
        }

        public override bool? UseItem(Player player)
        {
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            if (shardsPlayer.readingDisk != diskType)
            {
                shardsPlayer.readingDisk = diskType;
            }
            else
            {
                shardsPlayer.readingDisk = 0;
            }
            return true;
        }
    }

    public class AreusDataDisk : DataDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            diskType = (int)DiskID.Areus;
        }
    }

    public class AtheriaDataDisk : DataDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            diskType = (int)DiskID.Atheria;
        }
    }

    public class HardlightDataDisk : DataDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            diskType = (int)DiskID.Hardlight;
        }
    }

    public class NovaDataDisk : DataDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            diskType = (int)DiskID.Nova;
        }
    }

    public class TerrariansDataDisk : DataDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            diskType = (int)DiskID.Terrarian;
        }
    }

    public enum DiskID
    {
        None = 0,
        Areus = 1,
        Atheria = 2,
        Hardlight = 3,
        Nova = 4,
        Terrarian = 5
    }
}