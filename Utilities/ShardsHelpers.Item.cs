using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using System;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Utilities
{
    public partial class ShardsHelpers
    {
        public static void DefaultToPotion(this Item potion, int buff, int buffTime)
        {
            potion.useTime = 17;
            potion.useAnimation = 17;
            potion.useStyle = ItemUseStyleID.DrinkLiquid;
            potion.UseSound = SoundID.Item3;
            potion.consumable = true;
            potion.useTurn = true;

            potion.buffType = buff;
            potion.buffTime = buffTime;
            SoAGlobalItem.Potions.Add(potion.type);
        }

        public static void FixSwing(this Item item, Player player)
        {
            //Main.NewText(player.itemTime);
            //Main.NewText(player.toolTime, Color.Orange);
            //Main.NewText(player.itemAnimation + "|" + player.itemAnimationMax, Color.Beige);
            if (item.pick > 0 || item.axe > 0 || item.hammer > 0)
            {
                if ((player.toolTime > 0 && player.itemTime == 0) || !player.controlUseItem)
                    return;
                player.itemAnimation = Math.Min(player.itemAnimation, player.toolTime);
            }
            player.itemAnimation = player.itemAnimationMax;
        }

        /// <summary>
        /// Draws a basic single-frame glowmask for an item dropped in the world. Use in <see cref="Terraria.ModLoader.ModItem.PostDrawInWorld"/>
        /// </summary>
        public static void BasicInWorldGlowmask(this Item item, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, float rotation, float scale)
        {
            spriteBatch.Draw(
                glowTexture,
                new Vector2(
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - glowTexture.Height * 0.5f
                ),
                new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
                color,
                rotation,
                glowTexture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f);
        }

        public static void AddAreus(this Item item, bool dark = false)
        {
            item.type.AddAreusItem(dark);
            item.AddElementElec();
        }
        public static void AddAreusItem(this int id, bool dark)
        {
            SoAGlobalItem.AreusItem.Add(id, dark);
        }
        public static bool IsAreus(this Item item, bool includeDark)
        {
            if (!includeDark)
            {
                SoAGlobalItem.AreusItem.TryGetValue(item.type, out var dark);
                return SoAGlobalItem.AreusItem.ContainsKey(item.type) && !dark;
            }
            else return SoAGlobalItem.AreusItem.ContainsKey(item.type);
        }

        public static void AddUpgradable(this Item item)
        {
            item.type.AddUpgradableItem();
        }
        public static void AddUpgradableItem(this int id)
        {
            SoAGlobalItem.UpgradeableItem.Add(id);
        }
        public static bool IsUpgradable(this Item item)
        {
            return SoAGlobalItem.UpgradeableItem.Contains(item.type);
        }

        public static void AddEraser(this Item item)
        {
            item.type.AddEraserItem();
        }
        public static void AddEraserItem(this int id)
        {
            SoAGlobalItem.Eraser.Add(id);
        }

        public static void AddElementFire(this Item item)
        {
            SoA.TryElementCall("assignElement", item, 0);
        }
        public static void AddElementAqua(this Item item)
        {
            SoA.TryElementCall("assignElement", item, 1);
        }
        public static void AddElementElec(this Item item)
        {
            SoA.TryElementCall("assignElement", item, 2);
        }
        public static void AddElementWood(this Item item)
        {
            SoA.TryElementCall("assignElement", item, 3);
        }
    }
}
