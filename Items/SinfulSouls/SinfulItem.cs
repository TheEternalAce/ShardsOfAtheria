using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
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
                multiplier++;
            }
            if (NPC.downedGolemBoss)
            {
                multiplier++;
            }
            if (NPC.downedPlantBoss)
            {
                multiplier++;
            }
            if (NPC.downedMechBossAny)
            {
                multiplier++;
            }
            damage *= multiplier;
        }

        public bool SinfulItemUsable(Player player)
        {
            int soul = player.Sinful().SinfulSoulUsed;
            int requiredSinBuff = 0;
            if (RequiredSin > 0) requiredSinBuff = SinfulPlayer.SinfulBuffs[RequiredSin - 1];
            return RequiredSin == -1 || soul == requiredSinBuff;
        }

        public override bool CanUseItem(Player player)
        {
            return SinfulItemUsable(player);
        }
    }
}