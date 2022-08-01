using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class Necronomicon : SlayerItem
    {
        public int page;

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 56;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.life > 0 && npc.boss)
                    return false;
            }
            return page == 1;
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt))
                page = 0;
            else if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                page--;
            else page++;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (page > 0)
            {

            }

            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool? UseItem(Player player)
        {
            if (page == 1)
            {
                if (!player.GetModPlayer<SlayerPlayer>().slayerMode)
                {
                    Main.NewText("Slayer mode enabled");
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                    player.GetModPlayer<SlayerPlayer>().slayerMode = true;
                }
                else
                {
                    Main.NewText("Slayer mode disabled");
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                    player.GetModPlayer<SlayerPlayer>().slayerMode = false;
                }
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();

            if (page == 0)
            {
                tooltips.Add(new TooltipLine(Mod, "TableOfContents", "Table of Contents:"));
                tooltips.Add(new TooltipLine(Mod, "PageList", "General Info"));
                tooltips.Add(new TooltipLine(Mod, "PageList", "Soul Crystal Info"));

                // Absorbed Souls
                if (slayer.KingSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "King Slime")
                    {
                        OverrideColor = Color.Blue
                    };
                    tooltips.Add(line);
                }
                if (slayer.EyeSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Eye of Cthulhu")
                    {
                        OverrideColor = Color.Red
                    };
                    tooltips.Add(line);
                }
                if (slayer.BrainSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Brain of Cthulhu")
                    {
                        OverrideColor = Color.LightPink
                    };
                    tooltips.Add(line);
                }
                if (slayer.EaterSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Eater of Worlds")
                    {
                        OverrideColor = Color.Purple
                    };
                    tooltips.Add(line);
                }
                if (slayer.ValkyrieSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Nova Stellar, the Lightning Valkyrie")
                    {
                        OverrideColor = Color.DeepSkyBlue
                    };
                    tooltips.Add(line);
                }
                if (slayer.BeeSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Queen Bee")
                    {
                        OverrideColor = Color.Yellow
                    };
                    tooltips.Add(line);
                }
                if (slayer.SkullSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Skeletron")
                    {
                        OverrideColor = new Color(130, 130, 90)
                    };
                    tooltips.Add(line);
                }
                if (slayer.DeerclopsSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Deerclops")
                    {
                        OverrideColor = Color.MediumPurple
                    };
                    tooltips.Add(line);
                }
                if (slayer.WallSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Wall of Flesh")
                    {
                        OverrideColor = Color.MediumPurple
                    };
                    tooltips.Add(line);
                }
                if (slayer.QueenSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Queen Slime")
                    {
                        OverrideColor = Color.Pink
                    };
                    tooltips.Add(line);
                }
                if (slayer.DestroyerSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "The Destroyer")
                    {
                        OverrideColor = Color.Gray
                    };
                    tooltips.Add(line);
                }
                if (slayer.PrimeSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Skeletron Prime")
                    {
                        OverrideColor = Color.Gray
                    };
                    tooltips.Add(line);
                }
                if (slayer.TwinSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "The Twins")
                    {
                        OverrideColor = Color.Gray
                    };
                    tooltips.Add(line);
                }
                if (slayer.PlantSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Plantera")
                    {
                        OverrideColor = Color.Pink
                    };
                    tooltips.Add(line);
                }
                if (slayer.GolemSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Golem")
                    {
                        OverrideColor = Color.DarkOrange
                    };
                    tooltips.Add(line);
                }
                if (slayer.DukeSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Duke Fishron")
                    {
                        OverrideColor = Color.SeaGreen
                    };
                    tooltips.Add(line);
                }
                if (slayer.EmpressSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Empress of Light")
                    {
                        OverrideColor = Main.DiscoColor
                    };
                    tooltips.Add(line);
                }
                if (slayer.LunaticSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Lunatic Cultist")
                    {
                        OverrideColor = Color.Blue
                    };
                    tooltips.Add(line);
                }
                if (slayer.LordSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Moon Lord")
                    {
                        OverrideColor = Color.LightCyan
                    };
                    tooltips.Add(line);
                }
                if (slayer.LandSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Senterra, the Atherial Land")
                    {
                        OverrideColor = Color.Green
                    };
                    tooltips.Add(line);
                }
                if (slayer.TimeSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Genesis, Atherial Time")
                    {
                        OverrideColor = Color.BlueViolet
                    };
                    tooltips.Add(line);
                }
                if (slayer.DeathSoul)
                {
                    var line = new TooltipLine(Mod, "Souls", "Elizabeth Norman, Death")
                    {
                        OverrideColor = Color.DarkGray
                    };
                    tooltips.Add(line);
                }
            }
            if (page == 1)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "General Info:\n" +
                    "Use on this page to toggle Slayer mode. Cannot be used while a boss is alive.\n" +
                    "Bosses will drop every item in its loot table (in a stack of 1000 if the item is stackable), its slayer mode exclusive item and its Soul Crystal.\n" +
                    "This boss is considered slain and cannot be fought again.\n" +
                    "While Slayer mode is active, taking damage will reduce your defense. Defense will regenerate after a period of time."));
                if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
                {
                    tooltips.Add(new TooltipLine(Mod, "SlayerMode", "Slayer Mode Active")
                    {
                        OverrideColor = Color.Red
                    });
                }
            }
            if (page == 2)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Soul Crystal Info:\n" +
                    "Absorbing a boss' Soul Crystal gives you it's powers. Be warned: absorbing too many Soul Crystals may descend you into madness.\n" +
                    "Soul Crystals are removable by making a Soul Extracting Dagger.\n" +
                    "Certain combinations of Soul Crystals and the correct Soul Bond will cause a synergy between the Soul Crystals and new special effects will occur."));
            }

            // Soul Crystal effects
            if (page == 3)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "King Slime:\n" +
                    "Increased life and mana regen\n" +
                    "After taking damage, your next hit will heal 25% of that damage taken"));
            }
            if (page == 4)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Eye of Cthulhu:\n" +
                    "Summon 3 Servants when dashing, these Servants will chase down enemies\n" +
                    "Also creates an All Seeing Eye that lights up the cursor and marks enemies, making them take 10% more damage"));
            }
            if (page == 5)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Brain of Cthulhu:\n" +
                    "Spawns 4 Creepers\n" +
                    "While Creepers are alive you are invulnerable and cannot attack\n" +
                    "Gain a temporary 20% damage boost when all of the creepers die\n" +
                    "Creepers take 1 minute to respawn\n" +
                    "Cannot be immune to knockback"));
            }
            if (page == 6)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Eater of Worlds:\n" +
                    "Grants one revive and shoots a vile shot when using a weapon\n" +
                    "Revive has a 5 minute cooldown"));
            }
            if (page == 7)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Nova Stellar, the Lightning Valkyrie:\n" +
                    "Grants 8 defense, wing flight time boost and a dash that leaves behind an electric trail\n" +
                    "Attacks create 4 closing feather blades in an x pattern\n" +
                    "Getting hit by an enemy gives them Electric Shock"));
            }
            if (page == 8)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Queen Bee:\n" +
                    "Attacks inflict Poisoned and shoot stingers\n" +
                    "Spawn a bee every 10 seconds while in combat"));
            }
            if (page == 9)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Skeletron:\n" +
                    "While in combat enter a \"spin phase\" for 5 seconds every 30 seconds\n" +
                    "This \"spin phase\" inceases defense and damage by 50% and damages nearby enemies\n" +
                    "Attacks fire a homing skull"));
            }
            if (page == 10)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Deerclops:\n" +
                    "For every nearby NPC your damage is increased by 5% and your defense is increased by 10\n" +
                    "This increase caps at 15% increased damage and 15 defense\n" +
                    "Summons shadow hands when you are hurt"));
            }
            if (page == 11)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Wall of Flesh:\n" +
                    "Summon 5 friendly The Hungry over the course of 5 seconds"));
            }
            if (page == 12)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Queen Slime:\n" +
                    "Increased life and mana regen\n" +
                    "After taking damage, your next hit will heal 50% of that damage"));
            }
            if (page == 13)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "The Destroyer:\n" +
                    "Summon a Destroyer's Probe that fires at your cursor besides you\n" +
                    "Taking over 100 damage spawns another temporary Destroyer's Probe that will fire at nearby enemies\n" +
                    "You can have up to five of these \"attack probes\""));
            }
            if (page == 14)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Skeletron Prime:\n" +
                    "While in combat enter a \"spin phase\" for 5 seconds every 30 seconds\n" +
                    "This \"spin phase\" inceases defense and damage by 100% and damages nearby enemies\n" +
                    "Attacks fire either a laser, rocket or grenade"));
            }
            if (page == 15)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "The Twins:\n" +
                    "Projectiles inflict either Ichor or Cursed Inferno\n" +
                    "Melee hits inflict both debuffs\n" +
                    "Taking damage summons a shadow double of you to strike back with 100% of damage taken"));
            }
            if (page == 16)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Plantera:\n" +
                    "Spawn up to 8 tentacles over the course of 40 seconds\n" +
                    "Attacks fire a petal that inflicts venom\n" +
                    "Passive 15% increase in movement speed, 10% damage increase and increased life regen"));
            }
            if (page == 17)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Golem:\n" +
                    "Grants the effects of Shiny Stone\n" +
                    "While under 50% max life, gain increased life regen and summon a Golem head above you"));
            }
            if (page == 18)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Duke Fishron:\n" +
                    "Increased max flight time\n" +
                    "Summon a Sharknado over your head"));
                tooltips.Add(new TooltipLine(Mod, "SoulTeleport", string.Format("Press {0} to teleport", ShardsOfAtheria.SoulTeleport.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.SoulTeleport.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));

            }
            if (page == 19)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Empress of Light:\n" +
                    "Increased max flight time and permanent Shine and Night Owl buffs\n" +
                    "Daytime increased damage by 20% and nighttime increases defense by 20\n" +
                    "Hitting enemies summons a twilight lance for a second strike"));
            }
            if (page == 20)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Lunatic Cultist:\n" +
                    "Summons a magic circle behind you that fires ice fragments at your cursor\n" +
                    "Gives a chance to dodge attacks\n" +
                    "Every dodge increases ice fragments fired by 1, up to 5 total"));
                tooltips.Add(new TooltipLine(Mod, "SoulTeleport", string.Format("Press {0} to teleport", ShardsOfAtheria.SoulTeleport.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.SoulTeleport.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));
            }
            if (page == 21)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Moon Lord:\n" +
                    "Taking over 100 damage summons a True Eye of Cthulhu\n" +
                    "You can have up to 2 of these\n" +
                    "Another True EoC stays over you and attacks at your cursor"));
            }
            if (page == 22)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Senterra, the Atherial Land:\n" +
                    "This Soul Crystal needs further research. Please wait for the boss to be introduced into the mod."));
            }
            if (page == 23)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Genesis, Atherial Time:\n" +
                    "This Soul Crystal needs further research. Please wait for the boss to be introduced into the mod."));
            }
            if (page == 24)
            {
                tooltips.Add(new TooltipLine(Mod, "Page", "Elizabeth Norman, Death:\n" +
                    "This Soul Crystal needs further research. Please wait for the boss to be introduced into the mod."));
            }

            tooltips.Add(new TooltipLine(Mod, "TurnPage", "----------\n" +
                "Right click to turn the page"));
            tooltips.Add(new TooltipLine(Mod, "TurnPageBack", "Hold left shift and right click to turn the page backward"));
            if (page > 0)
                tooltips.Add(new TooltipLine(Mod, "TurnPageBack", "Hold left alt and right click to return to Table of Contents"));
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateInventory(Player player)
        {
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                if (page == 3 && !slayer.KingSoul)
                    page = 2;
                if (page == 4 && !slayer.EyeSoul)
                    page = 3;
                if (page == 5 && !slayer.BrainSoul)
                    page = 4;
                if (page == 6 && !slayer.EaterSoul)
                    page = 5;
                if (page == 7 && !slayer.ValkyrieSoul)
                    page = 6;
                if (page == 8 && !slayer.BeeSoul)
                    page = 7;
                if (page == 9 && !slayer.SkullSoul)
                    page = 8;
                if (page == 10 && !slayer.DeerclopsSoul)
                    page = 9;
                if (page == 11 && !slayer.WallSoul)
                    page = 10;
                if (page == 12 && !slayer.QueenSoul)
                    page = 11;
                if (page == 13 && !slayer.DestroyerSoul)
                    page = 12;
                if (page == 14 && !slayer.PrimeSoul)
                    page = 13;
                if (page == 15 && !slayer.TwinSoul)
                    page = 14;
                if (page == 16 && !slayer.PlantSoul)
                    page = 15;
                if (page == 17 && !slayer.GolemSoul)
                    page = 16;
                if (page == 18 && !slayer.DukeSoul)
                    page = 17;
                if (page == 19 && !slayer.EmpressSoul)
                    page = 18;
                if (page == 20 && !slayer.LunaticSoul)
                    page = 19;
                if (page == 21 && !slayer.LordSoul)
                    page = 20;
                if (page == 22 && !slayer.LandSoul)
                    page = 21;
                if (page == 23 && !slayer.TimeSoul)
                    page = 22;
                if ((page == 24 && !slayer.DeathSoul) || (page < 0 || page > 24))
                    page = 23;
            }
            else
            {
                if (page == 3 && !slayer.KingSoul)
                    page = 4;
                if (page == 4 && !slayer.EyeSoul)
                    page = 5;
                if (page == 5 && !slayer.BrainSoul)
                    page = 6;
                if (page == 6 && !slayer.EaterSoul)
                    page = 7;
                if (page == 7 && !slayer.ValkyrieSoul)
                    page = 8;
                if (page == 8 && !slayer.BeeSoul)
                    page = 9;
                if (page == 9 && !slayer.SkullSoul)
                    page = 10;
                if (page == 10 && !slayer.DeerclopsSoul)
                    page = 11;
                if (page == 11 && !slayer.WallSoul)
                    page = 12;
                if (page == 12 && !slayer.QueenSoul)
                    page = 13;
                if (page == 13 && !slayer.DestroyerSoul)
                    page = 14;
                if (page == 14 && !slayer.PrimeSoul)
                    page = 15;
                if (page == 15 && !slayer.TwinSoul)
                    page = 16;
                if (page == 16 && !slayer.PlantSoul)
                    page = 17;
                if (page == 17 && !slayer.GolemSoul)
                    page = 18;
                if (page == 18 && !slayer.DukeSoul)
                    page = 19;
                if (page == 19 && !slayer.EmpressSoul)
                    page = 20;
                if (page == 20 && !slayer.LunaticSoul)
                    page = 21;
                if (page == 21 && !slayer.LordSoul)
                    page = 22;
                if (page == 22 && !slayer.LandSoul)
                    page = 23;
                if (page == 23 && !slayer.TimeSoul)
                    page = 24;
                if ((page == 24 && !slayer.DeathSoul) || (page < 0 || page > 24))
                    page = 0;
            }
        }
    }
}