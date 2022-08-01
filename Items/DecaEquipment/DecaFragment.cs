using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public abstract class DecaFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unite with the other fragments to unlock their power");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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

    public class DecaFragmentA : DecaFragment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deca Fragment of Power");

            base.SetStaticDefaults();
        }
    }

    public class DecaFragmentB : DecaFragment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deca Fragment Resistance");

            base.SetStaticDefaults();
        }
    }

    public class DecaFragmentC : DecaFragment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deca Fragment of Mobility");

            base.SetStaticDefaults();
        }
    }

    public class DecaFragmentD : DecaFragment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deca Fragment of Utility");

            base.SetStaticDefaults();
        }
    }

    public class DecaFragmentE : DecaFragment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deca Fragment of No Limits");

            base.SetStaticDefaults();
        }
    }
}