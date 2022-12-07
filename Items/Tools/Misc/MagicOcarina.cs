using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class MagicOcarina : ModItem
    {
        public int selectedSong;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 26;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 2, 50);
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            OcarinaPlayer ocarinaPlayer = player.GetModPlayer<OcarinaPlayer>();

            if (player.altFunctionUse == 2)
            {
                Item.mana = 0;
                Item.healLife = 0;
                selectedSong++;
            }
            else
            {
                if (ocarinaPlayer.songsLearned == 0)
                {
                    return false;
                }
                // Time
                if (selectedSong == 0)
                {
                    Item.mana = 60;
                }
                // Healing
                if (selectedSong == 1)
                {
                    Item.mana = 100;
                    Item.healLife = player.statLifeMax2 / 5;
                }
                else
                {
                    Item.healLife = 0;
                }
                // Weather
                if (selectedSong == 2)
                {
                    Item.mana = 60;
                }
                // Healing Alt (Purification)
                if (selectedSong == 3)
                {
                    Item.mana = 50;
                }
                // Unhealing (Corruption/Crimson)
                if (selectedSong == 4)
                {
                    Item.mana = 50;
                }
                // Double Time
                if (selectedSong == 5)
                {
                    Item.mana = 50;
                }
                // Nature
                if (selectedSong == 6)
                {
                    Item.mana = 50;
                    Item.buffType = BuffID.DryadsWard;
                    Item.buffTime = 600;
                }
                // Soaring
                if (selectedSong == 7)
                {
                    Item.mana = 80;

                }
            }
            return ocarinaPlayer.songsLearned > 0;
        }

        public override bool? UseItem(Player player)
        {
            OcarinaPlayer ocarinaPlayer = player.GetModPlayer<OcarinaPlayer>();

            if (player.altFunctionUse != 2)
            {
                // Time
                if (ocarinaPlayer.timeSongLearned && selectedSong == 0)
                {
                    Main.dayTime = !Main.dayTime;
                    if (Main.dayTime)
                    {
                        Main.time = 7 * 3600 + 30 * 60;
                    }
                    else
                    {
                        Main.time = 4 * 3600 + 30 * 60;
                    }
                }
                // Weather
                if (ocarinaPlayer.weatherSongLearned && selectedSong == 2)
                {

                }
                // Healing Alt (Purification)
                if (ocarinaPlayer.healingAltSongLearned && selectedSong == 3)
                {

                }
                // Unhealing (Corruption/Crimson)
                if (ocarinaPlayer.unhealingSongLearned && selectedSong == 4)
                {

                }
                // Double Time
                if (ocarinaPlayer.doubleTimeSongLearned && selectedSong == 5)
                {
                    ModContent.GetInstance<OcarinaWorld>().doubleTimeSongUsed = !ModContent.GetInstance<OcarinaWorld>().doubleTimeSongUsed;
                }
                // Nature
                if (ocarinaPlayer.natureSongLearned && selectedSong == 6)
                {

                }
                // Soaring
                if (ocarinaPlayer.soaringSongLearned && selectedSong == 7)
                {

                }
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            OcarinaPlayer ocarinaPlayer = Main.LocalPlayer.GetModPlayer<OcarinaPlayer>();

            // Selected
            if (ocarinaPlayer.timeSongLearned && selectedSong == 0)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Time"));
            }
            if (ocarinaPlayer.healingSongLearned && selectedSong == 1)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Healing"));
            }
            if (ocarinaPlayer.weatherSongLearned && selectedSong == 2)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Weather"));
            }
            if (ocarinaPlayer.healingAltSongLearned && selectedSong == 3)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Healing (Alt)"));
            }
            if (ocarinaPlayer.unhealingSongLearned && selectedSong == 4)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Unhealing"));
            }
            if (ocarinaPlayer.doubleTimeSongLearned && selectedSong == 5)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Double Time"));
            }
            if (ocarinaPlayer.natureSongLearned && selectedSong == 6)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Nature"));
            }
            if (ocarinaPlayer.soaringSongLearned && selectedSong == 7)
            {
                tooltips.Add(new TooltipLine(Mod, "SelectedSong", "Song of Soaring"));
            }

            // Available
            tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Available Songs:"));
            if (ocarinaPlayer.songsLearned == 0)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "None"));
                return;
            }
            if (ocarinaPlayer.timeSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Time"));
            }
            if (ocarinaPlayer.healingSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Healing"));
            }
            if (ocarinaPlayer.weatherSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Weather"));
            }
            if (ocarinaPlayer.healingAltSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Healing (Alt)"));
            }
            if (ocarinaPlayer.unhealingSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Unhealing"));
            }
            if (ocarinaPlayer.doubleTimeSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Double Time"));
            }
            if (ocarinaPlayer.natureSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Nature"));
            }
            if (ocarinaPlayer.soaringSongLearned)
            {
                tooltips.Add(new TooltipLine(Mod, "AvailableSongs", "Song of Soaring"));
            }
        }

        public override void HoldItem(Player player)
        {
            UpdateInventory(player);
            base.HoldItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            OcarinaPlayer ocarinaPlayer = player.GetModPlayer<OcarinaPlayer>();
            if (!ocarinaPlayer.timeSongLearned && selectedSong == 0)
            {
                selectedSong++;
            }
            if (!ocarinaPlayer.healingSongLearned && selectedSong == 1)
            {
                selectedSong++;
            }
            if (!ocarinaPlayer.weatherSongLearned && selectedSong == 2)
            {
                selectedSong++;
            }
            if (!ocarinaPlayer.healingAltSongLearned && selectedSong == 3)
            {
                selectedSong++;
            }
            if (!ocarinaPlayer.unhealingSongLearned && selectedSong == 4)
            {
                selectedSong++;
            }
            if (!ocarinaPlayer.doubleTimeSongLearned && selectedSong == 5)
            {
                selectedSong++;
            }
            if (!ocarinaPlayer.natureSongLearned && selectedSong == 6)
            {
                selectedSong++;
            }
            if (!ocarinaPlayer.soaringSongLearned && selectedSong == 7 || selectedSong > 7)
            {
                selectedSong = 0;
            }
            base.UpdateInventory(player);
        }
    }

    public class OcarinaPlayer : ModPlayer
    {
        public int songsLearned = 0;

        public bool timeSongLearned = true;
        public bool healingSongLearned = true;
        public bool weatherSongLearned;
        public bool healingAltSongLearned;
        public bool unhealingSongLearned;
        public bool doubleTimeSongLearned;
        public bool natureSongLearned = true;
        public bool soaringSongLearned;
    }

    public class OcarinaWorld : ModSystem
    {
        public bool doubleTimeSongUsed;

        public override void PreUpdateTime()
        {
            if (doubleTimeSongUsed)
            {
                Main.dayRate = 2;
            }
            else Main.dayRate = 1;
        }
    }
}