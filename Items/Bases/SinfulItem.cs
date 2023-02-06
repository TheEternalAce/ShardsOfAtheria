using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Bases
{
    public abstract class SinfulItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.SinfulItem.Add(Type);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Item.damage > 0)
                tooltips.Add(new TooltipLine(Mod, "Damage", "Damage scales with progression"));
            var line = new TooltipLine(Mod, "Sinful", "Sinful")
            {
                OverrideColor = Color.Orange
            };
            tooltips.Add(line);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(damage);
        }
    }
}