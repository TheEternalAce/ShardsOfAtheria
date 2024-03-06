using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Items.Weapons;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.AreusComputer
{
    public class AreusComputerUI : UIState
    {
        UIPanel panel;
        ComputerTextBox textBox;
        int width = 1000;
        int height = 800;
        private static readonly Asset<Texture2D> PlayButtonTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay");
        private static readonly Asset<Texture2D> PowerButtonTexture = TextureAssets.Camera[5];

        public override void OnInitialize()
        {
            panel = new()
            {
                BackgroundColor = Color.Gold
            };
            panel.SetRectangle(0, 0, width, height);

            textBox = new("_");
            textBox.SetRectangle(0, 0, width - 72, height - 222);
            textBox.BackgroundColor = Color.Black;
            //textBox.TextColor = Color.Green;
            textBox.SetUnfocusKeys(false, true);
            panel.Append(textBox);

            var runButton = new UIHoverImageButton(PlayButtonTexture, "Run code.");
            runButton.OnLeftClick += (a, b) => RunCode();
            runButton.SetRectangle(width - 66, height - 122, 22, 22);
            panel.Append(runButton);

            var closeButton = new UIHoverImageButton(PowerButtonTexture, "Close terminal.");
            closeButton.OnLeftClick += (a, b) => CloseTerminal();
            closeButton.SetRectangle(width - 59, 22, 32, 32);
            panel.Append(closeButton);

            Append(panel);
        }

        public void CloseTerminal()
        {
            ModContent.GetInstance<AreusTerminalSystem>().HideTerminal();
        }

        public void RunCode()
        {
            bool cardActive = DevelopersKeyCard.CardActive(Main.LocalPlayer, out var _);
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<AreusTerminal>() || cardActive)
            {
                var computer = Main.LocalPlayer.HeldItem;
                string code = textBox.GetText();
                if (code.Length > 0)
                {
                    string codeClone = code.ToLower();
                    if (codeClone.Contains('_'))
                    {
                        codeClone = codeClone.Substring(codeClone.LastIndexOf('_') + 1);
                        codeClone = codeClone.TrimStart('\n');
                    }
                    if (codeClone.Length > 0)
                    {
                        string[] codeLines = codeClone.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        string output = "";
                        foreach (string line in codeLines)
                        {
                            string cloneLine = line.ToLower();
                            int lineLength = line.Length;
                            char[] lineArray = line.ToCharArray();
                            if (lineArray[lineLength - 1] == ';')
                            {
                                cloneLine = cloneLine.Replace(" ", "");
                                cloneLine = cloneLine.Trim(';');
                                // There is most definitely a better way to do all of this...
                                if (cloneLine.StartsWith("damage="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value <= 0)
                                        {
                                            value = 1;
                                        }
                                        if (value > 999 && !cardActive)
                                        {
                                            value = 999;
                                        }
                                        output += "\n[Output] Changed item damage to " + value + ".";
                                        computer.damage = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("damageclass=") || cloneLine.StartsWith("damagetype="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (cloneLine.Equals("melee") || cloneLine.Equals("damageclassmelee") || cloneLine.Equals("meleedamage"))
                                    {
                                        computer.DamageType = DamageClass.Melee;
                                    }
                                    else if (cloneLine.Equals("ranged") || cloneLine.Equals("damageclassranged") || cloneLine.Equals("rangeddamage"))
                                    {
                                        computer.DamageType = DamageClass.Ranged;
                                    }
                                    else if (cloneLine.Equals("magic") || cloneLine.Equals("damageclassmagic") || cloneLine.Equals("magicdamage"))
                                    {
                                        computer.DamageType = DamageClass.Magic;
                                    }
                                    else if (cloneLine.Equals("summon") || cloneLine.Equals("damageclasssummon") || cloneLine.Equals("summondamage"))
                                    {
                                        computer.DamageType = DamageClass.Summon;
                                    }
                                    else if (cloneLine.Equals("generic") || cloneLine.Equals("damageclassgeneric") || cloneLine.Equals("genericdamage") || cloneLine.Equals("none"))
                                    {
                                        computer.DamageType = DamageClass.Generic;
                                    }
                                    string damageClass = computer.DamageType.ToString();
                                    damageClass = damageClass.ToLower();
                                    damageClass = damageClass.Substring(damageClass.LastIndexOf('.') + 1);
                                    damageClass = damageClass.Replace("damageclass", "");
                                    output += "\n[Output] Changed item damage type to " + damageClass + " damage.";
                                }
                                else if (cloneLine.StartsWith("knockback="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    cloneLine = cloneLine.Trim('f');
                                    if (TryConvert(code, cloneLine, out float value))
                                    {
                                        if (value <= 0)
                                        {
                                            value = 1;
                                        }
                                        if (value > 999 && !cardActive)
                                        {
                                            value = 999;
                                        }
                                        output += "\n[Output] Changed item knock back to " + value + ".";
                                        computer.knockBack = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("usetime="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value <= 0)
                                        {
                                            value = 1;
                                        }
                                        if (value > 999 && !cardActive)
                                        {
                                            value = 999;
                                        }
                                        output += "\n[Output] Changed item use time to " + value + ".";
                                        computer.useTime = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("useanimation="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value <= 0)
                                        {
                                            value = 1;
                                        }
                                        if (value > 999 && !cardActive)
                                        {
                                            value = 999;
                                        }
                                        output += "\n[Output] Changed item use animation to " + value + ".";
                                        computer.useAnimation = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("reusedelay="))
                                {
                                    cloneLine = cloneLine[(cloneLine.LastIndexOf('=') + 1)..];
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value <= 0)
                                        {
                                            value = 1;
                                        }
                                        if (value > 999 && !cardActive)
                                        {
                                            value = 999;
                                        }
                                        output += "\n[Output] Changed item reuse delay to " + value + ".";
                                        computer.reuseDelay = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("crit="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value > 100 - 4)
                                        {
                                            value = 100 - 4;
                                        }
                                        output += "\n[Output] Changed item critical strike chance to " + (value + 4) + "%.";
                                        computer.crit = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("mana="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value > 100 - 4)
                                        {
                                            value = 100 - 4;
                                        }
                                        output += "\n[Output] Changed item mana cost to " + value + ".";
                                        computer.mana = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("shoot="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value <= 0)
                                        {
                                            value = 1;
                                        }
                                        if (value > ProjectileLoader.ProjectileCount - 1)
                                        {
                                            value = ProjectileLoader.ProjectileCount - 1;
                                        }
                                        ProjectileDefinition projectileDefinition = new(value);
                                        output += "\n[Output] Changed item projectile type to " + projectileDefinition.DisplayName + ".\n" +
                                            "(WARNING! Certain projectiles may lead to catastrophic results!)";
                                        computer.shoot = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("shootspeed="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    cloneLine = cloneLine.Trim('f');
                                    if (TryConvert(code, cloneLine, out float value))
                                    {
                                        if (value <= 0f)
                                        {
                                            value = 1f;
                                        }
                                        if (value > 999f && !cardActive)
                                        {
                                            value = 999f;
                                        }
                                        output += "\n[Output] Changed item projectile speed to " + value + ".";
                                        computer.shootSpeed = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("autoreuse=") || cloneLine.StartsWith("autoswing="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    cloneLine = cloneLine.Trim('f');
                                    if (TryConvert(code, cloneLine, out bool value))
                                    {
                                        output += "\n[Output] Changed item auto reuse to " + value + ".";
                                        computer.autoReuse = value;
                                    }
                                }
                                else if (cloneLine.StartsWith("type="))
                                {
                                    cloneLine = cloneLine.Substring(cloneLine.LastIndexOf('=') + 1);
                                    if (TryConvert(code, cloneLine, out int value))
                                    {
                                        if (value <= 0)
                                        {
                                            value = 1;
                                        }
                                        if (value > ItemLoader.ItemCount - 1)
                                        {
                                            value = ItemLoader.ItemCount - 1;
                                        }
                                        ItemDefinition itemDefinition = new(value);
                                        output += "\n[Output] Changed item projectile type to " + itemDefinition.DisplayName + "(type " + value + ").";
                                        computer.SetDefaults(value);
                                    }
                                }
                                else if (cloneLine.StartsWith("resetdefaults"))
                                {
                                    computer.SetDefaults(computer.type);
                                    output += "\n[Output] Reset item to default state.";
                                }
                                else if (cloneLine.StartsWith("damageclass") || cloneLine.StartsWith("damagetype"))
                                {
                                    string damageClass = computer.DamageType.ToString();
                                    damageClass = damageClass.ToLower();
                                    damageClass = damageClass.Substring(damageClass.LastIndexOf('.') + 1);
                                    damageClass = damageClass.Replace("damageclass", "");
                                    output += "\n[Output] Item deals " + damageClass + " damage.";
                                }
                                else if (cloneLine.StartsWith("damage"))
                                {
                                    output += "\n[Output] Item base damage is " + computer.damage + ".";
                                }
                                else if (cloneLine.StartsWith("knockback"))
                                {
                                    output += "\n[Output] Item base knockback is " + computer.knockBack + ".";
                                }
                                else if (cloneLine.StartsWith("usetime"))
                                {
                                    output += "\n[Output] Item use time is " + computer.useTime + ".";
                                }
                                else if (cloneLine.StartsWith("useanimation"))
                                {
                                    output += "\n[Output] Item use animation is " + computer.useAnimation + ".";
                                }
                                else if (cloneLine.StartsWith("reusedelay"))
                                {
                                    output += "\n[Output] Item reuse delay is " + computer.reuseDelay + ".";
                                }
                                else if (cloneLine.StartsWith("crit"))
                                {
                                    output += "\n[Output] Item base critical strike chance is " + (computer.crit + 4) + "%.";
                                }
                                else if (cloneLine.StartsWith("mana"))
                                {
                                    output += "\n[Output] Item base mana cost is " + computer.mana + ".";
                                }
                                else if (cloneLine.StartsWith("shootspeed"))
                                {
                                    output += "\n[Output] Item shoot speed is " + computer.shootSpeed + ".";
                                }
                                else if (cloneLine.StartsWith("shoot"))
                                {
                                    ProjectileDefinition projectileDefinition = new(computer.shoot);
                                    output += "\n[Output] Item projectile is " + projectileDefinition.DisplayName + " (type " + computer.shoot + ").";
                                }
                                else if (cloneLine.StartsWith("type"))
                                {
                                    ItemDefinition itemDefinition = new(computer.type);
                                    output += "\n[Output] Item type is " + itemDefinition.DisplayName + " (type " + computer.type + ").";
                                }
                                else if (cloneLine.StartsWith("autoreuse") || cloneLine.StartsWith("autoswing"))
                                {
                                    output += "\n[Output] Item auto reuse is set to " + computer.autoReuse + ".";
                                }
                            }
                            else
                            {
                                textBox.SetText(code + "\n[Error] ; expected._");
                                break;
                            }
                        }
                        textBox.currentString = code + output + "_\n";
                    }
                }
            }
        }

        public bool TryConvert(string code, string value, out int output)
        {
            output = 0;
            try
            {
                output = Convert.ToInt32(value);
            }
            catch
            {
                textBox.SetText(code + "\n[Error] Could not convert \"" + value + "\" to int._");
                return false;
            }
            return true;
        }
        public bool TryConvert(string code, string value, out float output)
        {
            output = 0;
            try
            {
                output = Convert.ToSingle(value);
            }
            catch
            {
                textBox.SetText(code + "\n[Error] Could not convert \"" + value + "\" to float._");
                return false;
            }
            return true;
        }
        public bool TryConvert(string code, string value, out bool output)
        {
            output = false;
            try
            {
                output = Convert.ToBoolean(value);
            }
            catch
            {
                textBox.SetText(code + "\n[Error] Could not convert \"" + value + "\" to bool._");
                return false;
            }
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (panel.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            int x = Main.screenWidth / 2 - width / 2;
            int y = Main.screenHeight / 2 - height / 2;
            panel.SetRectangle(x, y, width, height);
        }
    }

    public class AreusTerminalSystem : ModSystem
    {
        internal AreusComputerUI TerminalState;
        private UserInterface @interface;

        public override void Load()
        {
            TerminalState = new AreusComputerUI();
            TerminalState.Activate();
            @interface = new UserInterface();
            @interface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            @interface?.Update(gameTime);
        }

        public void ShowTerminal()
        {
            @interface.SetState(TerminalState);
        }
        public void HideTerminal()
        {
            @interface.SetState(null);
        }
        public bool TerminalActive => @interface.CurrentState != null;

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Areus Terminal",
                    delegate
                    {
                        @interface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
