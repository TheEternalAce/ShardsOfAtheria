using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Tools;
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
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<ExtractingSoul>();
            Item.shootSpeed = 16f;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(SoARecipes.EvilBar, 15)
                .AddIngredient(ItemID.Stinger, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
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
            if (player.GetModPlayer<SlayerPlayer>().soulCrystals == 0)
                CombatText.NewText(player.getRect(), Color.Blue, "You have no Soul Crystals to extract");
            else SelectSoul(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            var selected = new TooltipLine(Mod, "Verbose:RemoveMe", "Selected: None");

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
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Skeletron Prime")
                {
                    OverrideColor = Color.Gray
                };
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: The Twins")
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

            tooltips.Add(selected);

            //Available Souls
            if (player.GetModPlayer<SlayerPlayer>().KingSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "King Slime")
                {
                    OverrideColor = Color.Blue
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().EyeSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Eye of Cthulhu")
                {
                    OverrideColor = Color.Red
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().BrainSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Brain of Cthulhu")
                {
                    OverrideColor = Color.LightPink
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().EaterSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Eater of Worlds")
                {
                    OverrideColor = Color.Purple
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().ValkyrieSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Nova Stellar, the Lightning Valkyrie")
                {
                    OverrideColor = Color.DeepSkyBlue
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().BeeSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Queen Bee")
                {
                    OverrideColor = Color.Yellow
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().SkullSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Skeletron")
                {
                    OverrideColor = new Color(130, 130, 90)
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().DeerclopsSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Deerclops")
                {
                    OverrideColor = Color.MediumPurple
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().WallSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Wall of Flesh")
                {
                    OverrideColor = Color.MediumPurple
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().QueenSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Queen Slime")
                {
                    OverrideColor = Color.Pink
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().DestroyerSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "The Destroyer")
                {
                    OverrideColor = Color.Gray
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().PrimeSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Skeletron Prime")
                {
                    OverrideColor = Color.Gray
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().TwinSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "The Twins")
                {
                    OverrideColor = Color.Gray
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().PlantSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Plantera")
                {
                    OverrideColor = Color.Pink
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().GolemSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Golem")
                {
                    OverrideColor = Color.DarkOrange
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().DukeSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Duke Fishron")
                {
                    OverrideColor = Color.SeaGreen
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().EmpressSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Empress of Light")
                {
                    OverrideColor = Main.DiscoColor
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().LunaticSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Lunatic Cultist")
                {
                    OverrideColor = Color.Blue
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().LordSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Moon Lord")
                {
                    OverrideColor = Color.LightCyan
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().LandSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Senterra, the Atherial Land")
                {
                    OverrideColor = Color.Green
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().TimeSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Genesis, Atherial Time")
                {
                    OverrideColor = Color.BlueViolet
                };
                tooltips.Add(line);
            }
            if (player.GetModPlayer<SlayerPlayer>().DeathSoul)
            {
                var line = new TooltipLine(Mod, "AvailableSoul", "Elizabeth Norman, Death")
                {
                    OverrideColor = Color.DarkGray
                };
                tooltips.Add(line);
            }
        }

        public override bool CanUseItem(Player player)
        {
            return !player.immune && player.GetModPlayer<SlayerPlayer>().selectedSoul != SelectedSoul.None && player.GetModPlayer<SlayerPlayer>().soulCrystals > 0;
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
            Projectile.NewProjectile(source, spawnLocation, toPlayer * 4 + player.velocity, type, 50, 0, Main.myPlayer);
            return false;
        }

        public override void HoldItem(Player player)
        {
            SelecableSouls(player);
        }

        public override void UpdateInventory(Player player)
        {
            SelecableSouls(player);
        }
        
        public void SelecableSouls(Player player)
        {
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King && !player.GetModPlayer<SlayerPlayer>().KingSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Eye;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye && !player.GetModPlayer<SlayerPlayer>().EyeSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Brain;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain && !player.GetModPlayer<SlayerPlayer>().BrainSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Eater;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater && !player.GetModPlayer<SlayerPlayer>().EaterSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Valkyrie;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie && !player.GetModPlayer<SlayerPlayer>().ValkyrieSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Bee;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee && !player.GetModPlayer<SlayerPlayer>().BeeSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Skull;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull && !player.GetModPlayer<SlayerPlayer>().SkullSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Deerclops;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Deerclops && !player.GetModPlayer<SlayerPlayer>().DeerclopsSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Wall;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall && !player.GetModPlayer<SlayerPlayer>().WallSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Queen;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Queen && !player.GetModPlayer<SlayerPlayer>().QueenSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Destroyer;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer && !player.GetModPlayer<SlayerPlayer>().DestroyerSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Prime;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime && !player.GetModPlayer<SlayerPlayer>().PrimeSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Twins;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins && !player.GetModPlayer<SlayerPlayer>().TwinSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Plant;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant && !player.GetModPlayer<SlayerPlayer>().PlantSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Golem;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem && !player.GetModPlayer<SlayerPlayer>().GolemSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Duke;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke && !player.GetModPlayer<SlayerPlayer>().DukeSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Empress;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress && !player.GetModPlayer<SlayerPlayer>().EmpressSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Lunatic;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lunatic && !player.GetModPlayer<SlayerPlayer>().LunaticSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Lord;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord && !player.GetModPlayer<SlayerPlayer>().LordSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Senterrra;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra && !player.GetModPlayer<SlayerPlayer>().LandSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Genesis;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis && !player.GetModPlayer<SlayerPlayer>().TimeSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Death;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death && !player.GetModPlayer<SlayerPlayer>().DeathSoul)
                player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.King;

            if (player.GetModPlayer<SlayerPlayer>().soulCrystals == 0)
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