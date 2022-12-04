using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
using System;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Utilities
{
    public static class Element
    {
        public const int Fire = 0;
        public const int Ice = 1;
        public const int Electric = 2;
        public const int Metal = 3;
        public const int Areus = 4;
        public const int Frostfire = 5;
        public const int Hardlight = 6;
        public const int Organic = 7;
        public const int Plasma = 8;
    }
    public static class ShardsHelpers // General class full of helper methods
    {
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

        /// <summary>
        /// Sets NPC multipliers in the following order: Fire, Ice, Electric, Metal
        /// <para>This method should be called in the SetDefaults() override</para>
        /// </summary>
        public static void SetElementEffectivenessMultiplier(this NPC npc, params double[] multipliers)
        {
            if (multipliers.Length > 4)
            {
                throw new ArgumentException("Too many arguments, please use only four (4) decimal values");
            }
            else if (multipliers.Length < 4)
            {
                throw new ArgumentException("Too few arguments, please provide only four (4) decimal values");
            }
            for (int i = 0; i < multipliers.Length; i++)
            {
                npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[i] = multipliers[i];
            }
        }

        /// <summary>
        /// Sets NPC multipliers based on NPC Element
        /// <para>Base Elements:</para>
        /// <para>Fire NPC -> </para>
        /// <para>Ice NPC -> </para>
        /// <para>Metal NPC -> </para>
        /// <para>Electric NPC -> </para>
        /// <para>Sub-Elements:</para>
        /// <para>Areus NPC -> </para>
        /// <para>Frostfire NPC -> </para>
        /// <para>Hardlight NPC -> </para>
        /// <para>Organic NPC -> </para>
        /// <para>Plasma NPC -> </para>
        /// <para>Ice NPC -> </para>
        /// <para>0 = Fire, 1 = Ice, 2 = Electric, 3 = Metal, 4 = Areus, 5 = Frostfire, 6 = Hardlight, 7 = Organic, 8 = Plasma</para>
        /// <para>This method should be called in the SetDefaults() override</para>
        /// </summary>
        public static void SetElementEffectivenessByElement(this NPC npc, int element)
        {
            switch (element)
            {
                case Element.Fire:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 1.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 0.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 2.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 1.0;
                    break;
                case Element.Ice:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 2.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 1.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 1.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 0.5;
                    break;
                case Element.Electric:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 0.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 1.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 1.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 2.0;
                    break;
                case Element.Metal:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 1.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 2.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 0.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 1.0;
                    break;
                case Element.Areus:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 1.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 1.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 0.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 0.5;
                    break;
                case Element.Frostfire:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 0.8;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 0.8;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 1.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 0.8;
                    break;
                case Element.Hardlight:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 0.75;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 1.8;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 0.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 0.5;
                    break;
                case Element.Organic:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 2.0;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 0.8;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 1.5;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 1.0;
                    break;
                case Element.Plasma:
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Fire] = 0.8;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Ice] = 0.8;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Electric] = 0.8;
                    npc.GetGlobalNPC<SoAGlobalNPC>().elementMultiplier[Element.Metal] = 0.5;
                    break;
            }
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

        public static Color UseA(this Color color, int alpha) => new Color(color.R, color.G, color.B, alpha);
        public static Color UseA(this Color color, float alpha) => new Color(color.R, color.G, color.B, (int)(alpha * 255));

        public static SoAPlayer ShardsOfAtheria(this Player player)
        {
            return player.GetModPlayer<SoAPlayer>();
        }

        public static float CappedMeleeScale(this Player player)
        {
            var item = player.HeldItem;
            return Math.Clamp(player.GetAdjustedItemScale(item), 0.5f * item.scale, 2f * item.scale);
        }

        public static void CappedMeleeScale(Projectile proj)
        {
            float scale = Main.player[proj.owner].CappedMeleeScale();
            if (scale != 1f)
            {
                proj.scale *= scale;
                proj.width = (int)(proj.width * proj.scale);
                proj.height = (int)(proj.height * proj.scale);
            }
        }

        public static float Wave(float time, float minimum, float maximum)
        {
            return minimum + ((float)Math.Sin(time) + 1f) / 2f * (maximum - minimum);
        }
    }
}
