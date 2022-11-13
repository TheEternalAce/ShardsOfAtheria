using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
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
            if (NPC.downedMechBoss1)
            {
                damage += .1f;
            }
            if (NPC.downedMechBoss2)
            {
                damage += .1f;
            }
            if (NPC.downedMechBoss3)
            {
                damage += .1f;
            }
            if (NPC.downedPlantBoss)
            {
                damage += .15f;
            }
            if (NPC.downedGolemBoss)
            {
                damage += .15f;
            }
            if (NPC.downedFishron)
            {
                damage += .2f;
            }
            if (NPC.downedEmpressOfLight)
            {
                damage += .2f;
            }
            if (NPC.downedAncientCultist)
            {
                damage += .5f;
            }
            if (NPC.downedMoonlord)
            {
                damage += .5f;
            }
        }
    }
}