using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Common.Items
{
    public abstract class VirtuousItem : ModItem
    {
        public virtual int RequiredVirtue => -1;

        /// <summary>
        /// Tiers:<br/>
        /// 2 - Post Moon Lord<br/>
        /// 1 - Post Golem<br/>
        /// 0 - Hardmode<br/>
        /// </summary>
        public virtual int[] DamageSpread => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemDefaults.RarityVirtuous;
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

        public override bool CanUseItem(Player player)
        {
            int virtue = player.CardinalSoul().cardinalSoul;
            return RequiredVirtue == virtue && !player.CardinalSoul().Sinner;
        }
    }
}