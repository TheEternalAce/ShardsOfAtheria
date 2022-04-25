using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public abstract class DecaFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unite with the other fragments to unlock their power");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
            line = new TooltipLine(Mod, "Deca Fragment", "Deca Fragment")
            {
                OverrideColor = Color.Orange
            };
            tooltips.Add(line);
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Red;
            Item.width = 26;
            Item.height = 26;
        }
    }
}