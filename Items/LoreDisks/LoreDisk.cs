using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.LoreDisks
{
    abstract class LoreDisk : ModItem
    {
        public string path = "AtheriaLoreDisk";

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

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> lore = ModContent.Request<Texture2D>("ShardsOfAtheria/Items/LoreDisks/" + path);
            spriteBatch.Draw(lore.Value, position, null, drawColor * 0.95f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Asset<Texture2D> lore = ModContent.Request<Texture2D>("ShardsOfAtheria/Items/LoreDisks/" + path);
            Item.BasicInWorldGlowmask(spriteBatch, lore.Value, lightColor, rotation, scale);
        }
    }
}