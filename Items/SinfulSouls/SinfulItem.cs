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

        /// <summary>
        /// Tiers:<br/>
        /// 2 - Post Moon Lord<br/>
        /// 1 - Post Golem<br/>
        /// 0 - Hardmode<br/>
        /// </summary>
        public abstract int[] DamageSpread { get; }

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
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (DamageSpread.Length > 0)
                damage.Flat += ShardsHelpers.ProggressionValue(player, DamageSpread, 2);
            base.ModifyWeaponDamage(player, ref damage);
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