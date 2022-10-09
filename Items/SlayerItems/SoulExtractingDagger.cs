using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Tools;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class SoulExtractingDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to cycle between absorbed Soul Crystals\n" +
                "Use to extract the currently selected Soul Crystal");

            SoAGlobalItem.SlayerItem.Add(Type);

            base.SetStaticDefaults();
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ShardsRecipes.EvilBar, 15)
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
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
            if (slayer.soulCrystals.Count == 0)
                CombatText.NewText(player.getRect(), Color.Blue, "You have no Soul Crystals to extract");
            else SelectSoul(slayer);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
            var selected = new TooltipLine(Mod, "Verbose:RemoveMe", "Selected: None");

            //Selected Soul
            if (slayer.selectedSoul == SelectedSoul.King)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: King Slime")
                {
                    OverrideColor = Color.Blue
                };
            if (slayer.selectedSoul == SelectedSoul.Eye)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Eye of Cthulhu")
                {
                    OverrideColor = Color.Red
                };
            if (slayer.selectedSoul == SelectedSoul.Brain)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Brain of Cthulhu")
                {
                    OverrideColor = Color.LightPink
                };
            if (slayer.selectedSoul == SelectedSoul.Eater)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Eater of Worlds")
                {
                    OverrideColor = Color.Purple
                };
            if (slayer.selectedSoul == SelectedSoul.Valkyrie)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Nova Stellar, the Lightning Valkyrie")
                {
                    OverrideColor = Color.DeepSkyBlue
                };
            if (slayer.selectedSoul == SelectedSoul.Bee)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Queen Bee")
                {
                    OverrideColor = Color.Yellow
                };
            if (slayer.selectedSoul == SelectedSoul.Skull)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Skeletron")
                {
                    OverrideColor = new Color(130, 130, 90)
                };
            if (slayer.selectedSoul == SelectedSoul.Deerclops)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Deerclops")
                {
                    OverrideColor = Color.MediumPurple
                };
            if (slayer.selectedSoul == SelectedSoul.Wall)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Wall of Flesh")
                {
                    OverrideColor = Color.MediumPurple
                };
            if (slayer.selectedSoul == SelectedSoul.Queen)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Queen Slime")
                {
                    OverrideColor = Color.Pink
                };
            if (slayer.selectedSoul == SelectedSoul.Destroyer)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: The Destroyer")
                {
                    OverrideColor = Color.Gray
                };
            if (slayer.selectedSoul == SelectedSoul.Prime)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Skeletron Prime")
                {
                    OverrideColor = Color.Gray
                };
            if (slayer.selectedSoul == SelectedSoul.Twins)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: The Twins")
                {
                    OverrideColor = Color.Gray
                };
            if (slayer.selectedSoul == SelectedSoul.Plant)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Plantera")
                {
                    OverrideColor = Color.Pink
                };
            if (slayer.selectedSoul == SelectedSoul.Golem)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Golem")
                {
                    OverrideColor = Color.DarkOrange
                };
            if (slayer.selectedSoul == SelectedSoul.Duke)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Duke Fishron")
                {
                    OverrideColor = Color.SeaGreen
                };
            if (slayer.selectedSoul == SelectedSoul.Empress)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Empress of Light")
                {
                    OverrideColor = Main.DiscoColor
                };
            if (slayer.selectedSoul == SelectedSoul.Lunatic)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Lunatic Cultist")
                {
                    OverrideColor = Color.Blue
                };
            if (slayer.selectedSoul == SelectedSoul.Lord)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Moon Lord")
                {
                    OverrideColor = Color.LightCyan
                };
            if (slayer.selectedSoul == SelectedSoul.Senterrra)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Senterra, the Atherial Land")
                {
                    OverrideColor = Color.Green
                };
            if (slayer.selectedSoul == SelectedSoul.Genesis)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Genesis, Atherial Time")
                {
                    OverrideColor = Color.BlueViolet
                };
            if (slayer.selectedSoul == SelectedSoul.Death)
                selected = new TooltipLine(Mod, "SelectedSoul", "Selected: Elizabeth Norman, Death")
                {
                    OverrideColor = Color.DarkGray
                };

            tooltips.Add(selected);

            //Available Souls
            for (int i = 0; i < slayer.soulCrystals.Count; i++)
            {
                if (slayer.soulCrystals.Contains(ModContent.ItemType<SoulCrystal>()))
                {
                    var line = new TooltipLine(Mod, "AvailableSoul", "Senterra, the Atherial Land")
                    {
                        OverrideColor = Color.Green
                    };
                    tooltips.Add(line);
                }
            }
            base.ModifyTooltips(tooltips);
        }

        public override bool CanUseItem(Player player)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
            return !player.immune && slayer.selectedSoul != SelectedSoul.None && slayer.soulCrystals.Count > 0;
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
            SelecableSouls(player.GetModPlayer<SlayerPlayer>());
        }

        public override void UpdateInventory(Player player)
        {
            SelecableSouls(player.GetModPlayer<SlayerPlayer>());
        }
        
        public void SelecableSouls(SlayerPlayer slayer)
        {
            if (slayer.selectedSoul == SelectedSoul.King && !slayer.soulCrystals.Contains(ModContent.ItemType<KingSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Eye;
            if (slayer.selectedSoul == SelectedSoul.Eye && !slayer.soulCrystals.Contains(ModContent.ItemType<EyeSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Brain;
            if (slayer.selectedSoul == SelectedSoul.Brain && !slayer.soulCrystals.Contains(ModContent.ItemType<BrainSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Eater;
            if (slayer.selectedSoul == SelectedSoul.Eater && !slayer.soulCrystals.Contains(ModContent.ItemType<EaterSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Valkyrie;
            if (slayer.selectedSoul == SelectedSoul.Valkyrie && !slayer.soulCrystals.Contains(ModContent.ItemType<ValkyrieSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Bee;
            if (slayer.selectedSoul == SelectedSoul.Bee && !slayer.soulCrystals.Contains(ModContent.ItemType<BeeSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Skull;
            if (slayer.selectedSoul == SelectedSoul.Skull && !slayer.soulCrystals.Contains(ModContent.ItemType<SkullSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Deerclops;
            if (slayer.selectedSoul == SelectedSoul.Deerclops && !slayer.soulCrystals.Contains(ModContent.ItemType<DeerclopsSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Wall;
            if (slayer.selectedSoul == SelectedSoul.Wall && !slayer.soulCrystals.Contains(ModContent.ItemType<WallSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Queen;
            if (slayer.selectedSoul == SelectedSoul.Queen && !slayer.soulCrystals.Contains(ModContent.ItemType<QueenSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Destroyer;
            if (slayer.selectedSoul == SelectedSoul.Destroyer && !slayer.soulCrystals.Contains(ModContent.ItemType<DestroyerSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Prime;
            if (slayer.selectedSoul == SelectedSoul.Prime && !slayer.soulCrystals.Contains(ModContent.ItemType<PrimeSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Twins;
            if (slayer.selectedSoul == SelectedSoul.Twins && !slayer.soulCrystals.Contains(ModContent.ItemType<TwinsSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Plant;
            if (slayer.selectedSoul == SelectedSoul.Plant && !slayer.soulCrystals.Contains(ModContent.ItemType<PlantSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Golem;
            if (slayer.selectedSoul == SelectedSoul.Golem && !slayer.soulCrystals.Contains(ModContent.ItemType<GolemSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Duke;
            if (slayer.selectedSoul == SelectedSoul.Duke && !slayer.soulCrystals.Contains(ModContent.ItemType<DukeSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Empress;
            if (slayer.selectedSoul == SelectedSoul.Empress && !slayer.soulCrystals.Contains(ModContent.ItemType<EmpressSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Lunatic;
            if (slayer.selectedSoul == SelectedSoul.Lunatic && !slayer.soulCrystals.Contains(ModContent.ItemType<LunaticSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Lord;
            if (slayer.selectedSoul == SelectedSoul.Lord && !slayer.soulCrystals.Contains(ModContent.ItemType<LordSoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Senterrra;
            if (slayer.selectedSoul == SelectedSoul.Senterrra && !slayer.soulCrystals.Contains(ModContent.ItemType<SoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Genesis;
            if (slayer.selectedSoul == SelectedSoul.Genesis && !slayer.soulCrystals.Contains(ModContent.ItemType<SoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.Death;
            if (slayer.selectedSoul == SelectedSoul.Death && !slayer.soulCrystals.Contains(ModContent.ItemType<SoulCrystal>()))
                slayer.selectedSoul = SelectedSoul.King;

            if (slayer.soulCrystals.Count == 0)
                slayer.selectedSoul = SelectedSoul.None;
            else if (slayer.selectedSoul == SelectedSoul.None)
                slayer.selectedSoul++;
        }

        public void SelectSoul(SlayerPlayer slayer)
        {
            slayer.selectedSoul++;
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
        public const int Prime = 12;
        public const int Twins = 13;
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