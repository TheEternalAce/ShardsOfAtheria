using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Globals.Elements;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsHelpers // General class full of helper methods
    {
        public static int DefaultMaxStack = 9999;

        public struct UpgrageMaterial
        {
            public Item item;
            public int requiredStack;

            public UpgrageMaterial(Item item, int stack)
            {
                this.item = item;
                requiredStack = stack;
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

        public static void SlayBoss(this NPC boss, Player player)
        {
            ShardsDownedSystem.slainBosses.Add(boss.type);
        }

        /// <summary>
        /// Sets NPC multipliers in the following order: Fire, Ice, Electric, Metal
        /// <para>This method should be called in the SetDefaults() override</para>
        /// </summary>
        public static void SetCustomElementMultipliers(this NPC npc, params double[] multipliers)
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
                npc.GetGlobalNPC<NPCElements>().elementMultiplier[i] = multipliers[i];
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
        public static void SetElementMultipliersByElement(this NPC npc, int element)
        {
            switch (element)
            {
                case Element.Fire:
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Fire] = 1.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Ice] = 0.5;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Electric] = 2.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Metal] = 1.0;
                    break;
                case Element.Ice:
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Fire] = 2.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Ice] = 1.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Electric] = 1.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Metal] = 0.5;
                    break;
                case Element.Electric:
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Fire] = 0.5;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Ice] = 1.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Electric] = 1.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Metal] = 2.0;
                    break;
                case Element.Metal:
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Fire] = 1.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Ice] = 2.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Electric] = 0.5;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Metal] = 1.0;
                    break;
                case Element.Areus:
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Fire] = 1.5;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Ice] = 1.0;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Electric] = 0.5;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Metal] = 0.5;
                    break;
                case Element.Frostfire:
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Fire] = 0.8;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Ice] = 0.8;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Electric] = 1.5;
                    npc.GetGlobalNPC<NPCElements>().elementMultiplier[Element.Metal] = 0.8;
                    break;
            }
        }

        public static void SetWeaknessToElement(this NPC npc, int element, double multiplier)
        {
            if (element < 4)
            {
                npc.GetGlobalNPC<NPCElements>().elementMultiplier[element] = multiplier;
            }
            else
            {
                throw new IndexOutOfRangeException("Only pass a base element.");
            }
        }

        /// <summary>
        /// Insert useful summary here
        /// </summary>
        /// <param name="player"> Item to be upgraded</param>
        /// <param name="materials"> an array of ShardsHelpers.UpgradeMaterials</param>
        /// <param name="result"> Item to upgrade into</param>
        public static void UpgradeItem(this NPC npc, Player player, int result, params UpgrageMaterial[] materials)
        {
            Main.npcChatCornerItem = result;
            if (CanUpgradeItem(player, materials))
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    player.inventory[player.FindItem(materials[i].item.type)].stack -= materials[i].requiredStack;
                }
                if (materials[0].item.ModItem is GenesisAndRagnarok)
                {
                    (player.inventory[player.FindItem(materials[0].item.type)].ModItem as GenesisAndRagnarok).upgrades++;
                }
                SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound
                if (result > 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), npc.getRect(), result);

                    if (result == ModContent.ItemType<GenesisAndRagnarok>())
                    {
                        GenesisAndRagnarok genesisAndRagnarok = player.inventory[player.FindItem(materials[0].item.type)].ModItem as GenesisAndRagnarok;
                        switch (genesisAndRagnarok.upgrades)
                        {
                            case 1:
                                Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok1");
                                break;
                            case 2:
                                Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok2");
                                break;
                            case 3:
                                Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok3");
                                break;
                            case 4:
                                Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok4");
                                break;
                            case 5:
                                Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok5");
                                break;
                        }
                    }
                    else if (result == ModContent.ItemType<AreusSaber>() || result == ModContent.ItemType<TheMourningStar>())
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeAreusWeapon");
                    }
                }
            }
            else
            {
                string insufficient = "I need the following items:\n";
                for (int i = 0; i < materials.Length; i++)
                {
                    Item item = null;
                    if (player.HasItem(materials[i].item.type))
                    {
                        item = player.inventory[player.FindItem(materials[i].item.type)];
                    }
                    insufficient += Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.NotEnoughMaterial", i++, materials[i].item.Name, materials[i].item.type,
                        materials[i].requiredStack, item == null ? 0 : item.stack) + "\n";
                    Main.npcChatText = insufficient;
                }
            }
        }

        public static bool CanUpgradeItem(Player player, UpgrageMaterial[] upgrageMaterials)
        {
            int requiredItems = upgrageMaterials.Length;
            int playerItems = 0;
            for (int i = 0; i < requiredItems; i++)
            {
                if (player.HasItem(upgrageMaterials[i].item.type))
                {
                    Item item = player.inventory[player.FindItem(upgrageMaterials[i].item.type)];
                    if (item.stack >= upgrageMaterials[i].requiredStack)
                    {
                        playerItems++;
                    }
                }
            }
            return playerItems >= requiredItems;
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

        internal static void LogSomething(string something)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(something), Color.White);
            Console.WriteLine(something);
        }
    }

    public static class Element
    {
        public const int Null = -1;
        public const int Fire = 0;
        public const int Ice = 1;
        public const int Electric = 2;
        public const int Metal = 3;
        public const int Areus = 4;
        public const int Frostfire = 5;
    }
}
