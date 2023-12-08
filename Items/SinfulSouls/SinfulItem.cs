using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public abstract class SinfulItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.SinfulItem.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.rare = ItemDefaults.RaritySinful;
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
            int multiplier = 1;
            if (NPC.downedMoonlord)
            {
                multiplier = 5;
            }
            else if (NPC.downedGolemBoss)
            {
                multiplier = 4;
            }
            damage *= multiplier;
        }
    }
}