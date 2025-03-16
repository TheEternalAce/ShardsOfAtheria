using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

        public static bool IsTool(this Item item)
        {
            return item.hammer > 0 || item.pick > 0 || item.axe > 0;
        }

        public static bool IsWeapon(this Item item)
        {
            return item.damage > 0 && !item.IsTool();
        }
        public static bool IsWeapon(this Item item, DamageClass damageClass)
        {
            return item.damage > 0 && !item.IsTool() && item.DamageType.CountsAsClass(damageClass);
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

        public static void AddAreus(this Item item, bool dark = false, bool forceAddElements = false)
        {
            item.type.AddAreusItem(dark);
            if (!dark || forceAddElements)
            {
                item.AddElement(2);
                item.AddRedemptionElement(7);
            }
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

        /// <summary>
        /// 0 (Acid) <br/>
        /// 1 (Bludgeoning) <br/>
        /// 2 (Cold) <br/>
        /// 3 (Fire) <br/>
        /// 4 (Force) <br/>
        /// 5 (Lightning) <br/>
        /// 6 (Necrotic) <br/>
        /// 7 (Piercing) <br/>
        /// 8 (Poison) <br/>
        /// 9 (Psychic) <br/>
        /// 10 (Radiant) <br/>
        /// 11 (Slashing) <br/>
        /// 12 (Thunder) <br/>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="elementIDs"></param>
        public static void AddDamageType(this Item item, params int[] elementIDs)
        {
            SoA.TryDungeonCall("addDamageElement", "item", item.type, elementIDs);
        }

        /// <summary>
        /// 0 (Fire) <br/>
        /// 1 (Aqua) <br/>
        /// 2 (Elec) <br/>
        /// 3 (Wood)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="elementID"></param>
        public static void AddElement(this Item item, int elementID)
        {
            SoA.TryElementCall("assignElement", item, elementID);
        }
        public static void AddElementTooltip(this Item item, string tooltip)
        {
            SoA.TryElementCall("addElementTooltip", item, tooltip);
        }
        public static void AddElementTooltipKey(this Item item, string key)
        {
            SoA.TryElementCall("addElementTooltipKey", item, key);
        }

        /// <summary>
        /// 1	(Arcane) <br/>
        /// 2	(Fire) <br/>
        /// 3	(Water) <br/>
        /// 4	(Ice) <br/>
        /// 5	(Earth) <br/>
        /// 6	(Wind) <br/>
        /// 7	(Thunder) <br/>
        /// 8	(Holy) <br/>
        /// 9	(Shadow) <br/>
        /// 10	(Nature) <br/>
        /// 11	(Poison) <br/>
        /// 12	(Blood) <br/>
        /// 13	(Psychic) <br/>
        /// 14	(Celestial) <br/>
        /// 15	(Exposive)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="elementID"></param>
        public static void AddRedemptionElement(this Item item, int elementID)
        {
            SoA.TryRedemptionCall("addElementItem", elementID, item.type);
        }

        public static void SetGunStats(this int itemID, string gunType, int ammo)
        {
            SoA.TryReloadableGunsCall("setguntype", itemID, gunType);
            SoA.TryReloadableGunsCall("setmaxammo", itemID, ammo);
        }
    }
}
