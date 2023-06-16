using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using System;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsItemHelper
    {
        public static class Rare
        {
            public static readonly int EarlyGame = ItemRarityID.White;
            public static readonly int PostEye = ItemRarityID.Blue;
            public static readonly int PreSkeletron = ItemRarityID.Green;
            public static readonly int PreWall = ItemRarityID.Orange;
            public static readonly int EarlyHardmode = ItemRarityID.LightRed;
            public static readonly int PostMech = ItemRarityID.Pink;
            public static readonly int PrePlantera = ItemRarityID.LightPurple;
            public static readonly int PreGolem = ItemRarityID.Lime;
            public static readonly int PreCultist = ItemRarityID.Yellow;
            public static readonly int PreML = ItemRarityID.Cyan;
            public static readonly int PostML = ItemRarityID.Red;
        }

        public static class Value
        {
            public static readonly int EarlyGame = 0;
            public static readonly int PostEye = 0;
            public static readonly int PreSkeletron = 0;
            public static readonly int PreWall = 0;
            public static readonly int EarlyHardmode = 0;
            public static readonly int PostMech = 0;
            public static readonly int PrePlantera = 0;
            public static readonly int PreGolem = 0;
            public static readonly int PreCultist = 0;
            public static readonly int PreML = 0;
            public static readonly int PostML = 0;
        }

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
            item.AddElec();
        }
        public static void AddAreusItem(this int itemID, bool dark)
        {
            if (dark)
            {
                SoAGlobalItem.DarkAreusWeapon.Add(itemID);
            }
            else
            {
                SoAGlobalItem.AreusWeapon.Add(itemID);
            }
        }
    }
}
