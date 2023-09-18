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
        public static readonly int EarlyGameRarity = ItemRarityID.White;
        public static readonly int PostEyeRarity = ItemRarityID.Blue;
        public static readonly int PreSkeletronRarity = ItemRarityID.Green;
        public static readonly int PreWallRarity = ItemRarityID.Orange;
        public static readonly int EarlyHardmodeRarity = ItemRarityID.LightRed;
        public static readonly int PostMechRarity = ItemRarityID.Pink;
        public static readonly int PrePlanteraRarity = ItemRarityID.LightPurple;
        public static readonly int PreGolemRarity = ItemRarityID.Lime;
        public static readonly int PreCultistRarity = ItemRarityID.Yellow;
        public static readonly int PreMLRarity = ItemRarityID.Cyan;
        public static readonly int PostMLRarity = ItemRarityID.Red;

        public static readonly int EarlyGameValue = 0;
        public static readonly int PostEyeValue = 0;
        public static readonly int PreSkeletronValue = 0;
        public static readonly int PreWallValue = 0;
        public static readonly int EarlyHardmodeValue = 0;
        public static readonly int PostMechValue = 0;
        public static readonly int PrePlanteraValue = 0;
        public static readonly int PreGolemValue = 0;
        public static readonly int PreCultistValue = 0;
        public static readonly int PreMLValue = 0;
        public static readonly int PostMLValue = 0;

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
        public static bool IsAreus(this Item item)
        {
            return SoAGlobalItem.AreusWeapon.Contains(item.type) ||
                SoAGlobalItem.DarkAreusWeapon.Contains(item.type);
        }

        public static bool IsUpgradable(this Item item)
        {
            return SoAGlobalItem.UpgradeableItem.Contains(item.type);
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
