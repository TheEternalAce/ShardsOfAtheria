using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Tools;
using Terraria.Audio;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class SoulExtractingDagger : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to cycle between absorbed Soul Crystals\n" +
                "Use to extract the currently selected Soul Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 40;
            Item.rare = ItemRarityID.Yellow;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<ExtractingSoul>();
            Item.shootSpeed = 16f;
            Item.noUseGraphic = true;
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
            SelectSoul(player);
            if (!player.GetModPlayer<SlayerPlayer>().anySoulCrystals)
                CombatText.NewText(player.getRect(), Color.Blue, "You have no Soul Crystals to extract");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            var selected = new TooltipLine(Mod, "Verbose:RemoveMe", "Selected: None");
            var line = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line1 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line2 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line3 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line4 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line5 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line6 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line7 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line8 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line9 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line10 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line11 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line12 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line13 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line14 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line15 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line16 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line17 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line18 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line19 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line20 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");
            var line21 = new TooltipLine(Mod, "Verbose:RemoveMe", "Unavailable");

            //Selected Soul
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: King Slime")
                {
                    OverrideColor = Color.Blue
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Eye of Cthulhu")
                {
                    OverrideColor = Color.Red
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Brain of Cthulhu")
                {
                    OverrideColor = Color.LightPink
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Eater of Worlds")
                {
                    OverrideColor = Color.Purple
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Nova Stellar, the Lightning Valkyrie")
                {
                    OverrideColor = Color.DeepSkyBlue
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Queen Bee")
                {
                    OverrideColor = Color.Yellow
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Skeletron")
                {
                    OverrideColor = new Color(130, 130, 90)
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Deerclops)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Deerclops")
                {
                    OverrideColor = Color.MediumPurple
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Wall of Flesh")
                {
                    OverrideColor = Color.MediumPurple
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Queen)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Queen Slime")
                {
                    OverrideColor = Color.Pink
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: The Destroyer")
                {
                    OverrideColor = Color.Gray
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: The Twins")
                {
                    OverrideColor = Color.Gray
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Skeletron Prime")
                {
                    OverrideColor = Color.Gray
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Plantera")
                {
                    OverrideColor = Color.Pink
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Golem")
                {
                    OverrideColor = Color.DarkOrange
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Duke Fishron")
                {
                    OverrideColor = Color.SeaGreen
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Empress of Light")
                {
                    OverrideColor = Main.DiscoColor
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lunatic)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Lunatic Cultist")
                {
                    OverrideColor = Color.Blue
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Moon Lord")
                {
                    OverrideColor = Color.LightCyan
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Senterra, the Atherial Land")
                {
                    OverrideColor = Color.Green
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Genesis, Atherial Time")
                {
                    OverrideColor = Color.BlueViolet
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Elizabeth Norman, Death")
                {
                    OverrideColor = Color.DarkGray
                };

            //Available Souls
            if (player.GetModPlayer<SlayerPlayer>().KingSoul == SoulCrystalStatus.Absorbed)
            {
                line = new TooltipLine(Mod, "AvailableSoul", "King Slime")
                {
                    OverrideColor = Color.Blue
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().EyeSoul == SoulCrystalStatus.Absorbed)
            {
                line1 = new TooltipLine(Mod, "AvailableSoul", "Eye of Cthulhu")
                {
                    OverrideColor = Color.Red
                };
                tooltips.Add(line1);
            }
            if (player.GetModPlayer<SlayerPlayer>().BrainSoul == SoulCrystalStatus.Absorbed)
            {
                line2 = new TooltipLine(Mod, "AvailableSoul", "Brain of Cthulhu")
                {
                    OverrideColor = Color.LightPink
                };
                tooltips.Add(line2);
            }
            if (player.GetModPlayer<SlayerPlayer>().EaterSoul == SoulCrystalStatus.Absorbed)
            {
                line3 = new TooltipLine(Mod, "AvailableSoul", "Eater of Worlds")
                {
                    OverrideColor = Color.Purple
                };
                tooltips.Add(line3);
            }
            if (player.GetModPlayer<SlayerPlayer>().ValkyrieSoul == SoulCrystalStatus.Absorbed)
            {
                line4 = new TooltipLine(Mod, "AvailableSoul", "Nova Stellar, the Lightning Valkyrie")
                {
                    OverrideColor = Color.DeepSkyBlue
                };
                tooltips.Add(line4);
            }
            if (player.GetModPlayer<SlayerPlayer>().BeeSoul == SoulCrystalStatus.Absorbed)
            {
                line5 = new TooltipLine(Mod, "AvailableSoul", "Queen Bee")
                {
                    OverrideColor = Color.Yellow
                };
                tooltips.Add(line5);
            }
            if (player.GetModPlayer<SlayerPlayer>().SkullSoul == SoulCrystalStatus.Absorbed)
            {
                line6 = new TooltipLine(Mod, "AvailableSoul", "Skeletron")
                {
                    OverrideColor = new Color(130, 130, 90)
                };
                tooltips.Add(line6);
            }
            if (player.GetModPlayer<SlayerPlayer>().DeerclopsSoul == SoulCrystalStatus.Absorbed)
            {
                line7 = new TooltipLine(Mod, "AvailableSoul", "Deerclops")
                {
                    OverrideColor = Color.MediumPurple
                };
                tooltips.Add(line7);
            }
            if (player.GetModPlayer<SlayerPlayer>().WallSoul == SoulCrystalStatus.Absorbed)
            {
                line8 = new TooltipLine(Mod, "AvailableSoul", "Wall of Flesh")
                {
                    OverrideColor = Color.MediumPurple
                };
                tooltips.Add(line8);
            }
            if (player.GetModPlayer<SlayerPlayer>().QueenSoul == SoulCrystalStatus.Absorbed)
            {
                line9 = new TooltipLine(Mod, "AvailableSoul", "QueenSlime")
                {
                    OverrideColor = Color.Pink
                };
                tooltips.Add(line9);
            }
            if (player.GetModPlayer<SlayerPlayer>().DestroyerSoul == SoulCrystalStatus.Absorbed)
            {
                line10 = new TooltipLine(Mod, "AvailableSoul", "The Destroyer")
                {
                    OverrideColor = Color.Gray
                };
                tooltips.Add(line10);
            }
            if (player.GetModPlayer<SlayerPlayer>().TwinSoul == SoulCrystalStatus.Absorbed)
            {
                line11 = new TooltipLine(Mod, "AvailableSoul", "The Twins")
                {
                    OverrideColor = Color.Gray
                };
                tooltips.Add(line11);
            }
            if (player.GetModPlayer<SlayerPlayer>().PrimeSoul == SoulCrystalStatus.Absorbed)
            {
                line12 = new TooltipLine(Mod, "AvailableSoul", "Skeletron Prime")
                {
                    OverrideColor = Color.Gray
                };
                tooltips.Add(line12);
            }
            if (player.GetModPlayer<SlayerPlayer>().PlantSoul == SoulCrystalStatus.Absorbed)
            {
                line13 = new TooltipLine(Mod, "AvailableSoul", "Plantera")
                {
                    OverrideColor = Color.Pink
                };
                tooltips.Add(line13);
            }
            if (player.GetModPlayer<SlayerPlayer>().GolemSoul == SoulCrystalStatus.Absorbed)
            {
                line14 = new TooltipLine(Mod, "AvailableSoul", "Golem")
                {
                    OverrideColor = Color.DarkOrange
                };
                tooltips.Add(line14);
            }
            if (player.GetModPlayer<SlayerPlayer>().DukeSoul == SoulCrystalStatus.Absorbed)
            {
                line15 = new TooltipLine(Mod, "AvailableSoul", "Duke Fishron")
                {
                    OverrideColor = Color.SeaGreen
                };
                tooltips.Add(line15);
            }
            if (player.GetModPlayer<SlayerPlayer>().EmpressSoul == SoulCrystalStatus.Absorbed)
            {
                line16 = new TooltipLine(Mod, "AvailableSoul", "Empress of Light")
                {
                    OverrideColor = Main.DiscoColor
                };
                tooltips.Add(line16);
            }
            if (player.GetModPlayer<SlayerPlayer>().LunaticSoul == SoulCrystalStatus.Absorbed)
            {
                line17 = new TooltipLine(Mod, "AvailableSoul", "Lunatic Cultist")
                {
                    OverrideColor = Color.Blue
                };
                tooltips.Add(line17);
            }
            if (player.GetModPlayer<SlayerPlayer>().LordSoul == SoulCrystalStatus.Absorbed)
            {
                line18 = new TooltipLine(Mod, "AvailableSoul", "Moon Lord")
                {
                    OverrideColor = Color.LightCyan
                };
                tooltips.Add(line18);
            }
            if (player.GetModPlayer<SlayerPlayer>().LandSoul == SoulCrystalStatus.Absorbed)
            {
                line19 = new TooltipLine(Mod, "AvailableSoul", "Senterra, the Atherial Land")
                {
                    OverrideColor = Color.Green
                };
                tooltips.Add(line19);
            }
            if (player.GetModPlayer<SlayerPlayer>().TimeSoul == SoulCrystalStatus.Absorbed)
            {
                line20 = new TooltipLine(Mod, "AvailableSoul", "Genesis, Atherial Time")
                {
                    OverrideColor = Color.BlueViolet
                };
                tooltips.Add(line20);
            }
            if (player.GetModPlayer<SlayerPlayer>().DeathSoul == SoulCrystalStatus.Absorbed)
            {
                line21 = new TooltipLine(Mod, "AvailableSoul", "Elizabeth Norman, Death")
                {
                    OverrideColor = Color.DarkGray
                };
                tooltips.Add(line21);
            }

            tooltips.Add(selected);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SlayerPlayer>().selectedSoul != SelectedSoul.None && player.GetModPlayer<SlayerPlayer>().anySoulCrystals;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 toPlayer;
            Vector2 spawnLocation;
            if (player.direction == 1)
                spawnLocation = player.Center + new Vector2(48, 0);
            else spawnLocation = player.Center + new Vector2(-48, 0);
            if (player.direction == 1)
                toPlayer = player.Center + new Vector2(-48, 0);
            else toPlayer = player.Center + new Vector2(48, 0);
            Projectile.NewProjectile(source, spawnLocation, toPlayer, type, 0, 0, player.whoAmI);
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King && player.GetModPlayer<SlayerPlayer>().KingSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Eye;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye && player.GetModPlayer<SlayerPlayer>().EyeSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Brain;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain && player.GetModPlayer<SlayerPlayer>().BrainSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Eater;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater && player.GetModPlayer<SlayerPlayer>().EaterSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Valkyrie;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie && player.GetModPlayer<SlayerPlayer>().ValkyrieSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Bee;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee && player.GetModPlayer<SlayerPlayer>().BeeSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Skull;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull && player.GetModPlayer<SlayerPlayer>().BeeSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Deerclops;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Deerclops && player.GetModPlayer<SlayerPlayer>().DeerclopsSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Wall;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall && player.GetModPlayer<SlayerPlayer>().WallSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Destroyer;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer && player.GetModPlayer<SlayerPlayer>().DestroyerSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Twins;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins && player.GetModPlayer<SlayerPlayer>().TwinSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Prime;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime && player.GetModPlayer<SlayerPlayer>().PrimeSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Plant;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant && player.GetModPlayer<SlayerPlayer>().PlantSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Golem;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem && player.GetModPlayer<SlayerPlayer>().GolemSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Duke;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke && player.GetModPlayer<SlayerPlayer>().DukeSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Empress;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress && player.GetModPlayer<SlayerPlayer>().EmpressSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Lunatic;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lunatic && player.GetModPlayer<SlayerPlayer>().LunaticSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Lord;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord && player.GetModPlayer<SlayerPlayer>().LordSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Senterrra;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra && player.GetModPlayer<SlayerPlayer>().LandSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Genesis;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis && player.GetModPlayer<SlayerPlayer>().TimeSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Death;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death && player.GetModPlayer<SlayerPlayer>().DeathSoul != SoulCrystalStatus.Absorbed)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.King;

            if (!player.GetModPlayer<SlayerPlayer>().anySoulCrystals)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.None;
            else if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.None)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.King;
        }

        public void SelectSoul(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().selectedSoul++;
        }
    }

    public class SelectedSoul
    {
        public const int None = 0;
        public const int King = 1;
        public const int Eye = 2;
        public const int Brain = 3;
        public const int Eater = 4;
        public const int Valkyrie = 5;
        public const int Bee = 6;
        public const int Skull = 7;
        public const int Deerclops = 8;
        public const int Wall = 9;
        public const int Queen = 10;
        public const int Destroyer = 11;
        public const int Twins = 12;
        public const int Prime = 13;
        public const int Plant = 14;
        public const int Golem = 15;
        public const int Duke = 16;
        public const int Empress = 17;
        public const int Lunatic = 18;
        public const int Lord = 19;
        public const int Senterrra = 20;
        public const int Genesis = 21;
        public const int Death = 22;
    }
}