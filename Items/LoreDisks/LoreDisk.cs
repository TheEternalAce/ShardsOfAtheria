using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.LoreDisks
{
    public abstract class LoreDisk : ModItem
    {
        public int discType = 0;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ShardsConfigServerSide>().experimental;
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.maxStack = 1;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> lore = ModContent.Request<Texture2D>("ShardsOfAtheria/Items/LoreDisks/LoreDisk");
            spriteBatch.Draw(lore.Value, position, null, drawColor * 0.95f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Asset<Texture2D> lore = ModContent.Request<Texture2D>("ShardsOfAtheria/Items/LoreDisks/LoreDisk");
            Item.BasicInWorldGlowmask(spriteBatch, lore.Value, lightColor, rotation, scale);
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }

        public override bool? UseItem(Player player)
        {
            return null;
        }
    }

    public class AreusLoreDisk : LoreDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            discType = (int)DiskID.Areus;
        }
    }

    public class AtheriaLoreDisk : LoreDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            discType = (int)DiskID.Atheria;
        }
    }

    public class HardlightLoreDisk : LoreDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            discType = (int)DiskID.Hardlight;
        }
    }

    public class NovaLoreDisk : LoreDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            discType = (int)DiskID.Nova;
        }
    }

    public class TerrariansLoreDisk : LoreDisk
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            discType = (int)DiskID.Terrarian;
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