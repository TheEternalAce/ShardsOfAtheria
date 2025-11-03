using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public abstract class SinfulItem : ModItem
    {
        public abstract int RequiredSin { get; }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemDefaults.RaritySinful;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Item.damage > 0)
                tooltips.Add(new TooltipLine(Mod, "Damage", ShardsHelpers.LocalizeCommon("DamageScale")));
            var line = new TooltipLine(Mod, "Sinful", "Sinful")
            {
                OverrideColor = Color.Orange
            };
            tooltips.Add(line);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
        }

        public bool SinfulItemUsable(Player player)
        {
            int sin = player.Sinner().sinID;
            return RequiredSin == sin;
        }

        public override bool CanUseItem(Player player)
        {
            return SinfulItemUsable(player);
        }
    }
}