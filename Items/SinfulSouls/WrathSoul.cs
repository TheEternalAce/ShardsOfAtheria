using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class WrathSoul : SinfulSouls
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<WrathBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class WrathPlayer : ModPlayer
    {
        public float anger;
        public int rage;
        public int calming;

        public override void PreUpdate()
        {
            if (!Player.HasBuff(ModContent.BuffType<WrathBuff>()))
            {
                anger = 0f;
                rage = 0;
            }
            if (Player.ShardsOfAtheria().combatTimer == 0 && (anger > 0 || rage > 0))
            {
                calming++;
                if (calming == 60)
                {
                    calming = 0;
                    if (anger > 0)
                        anger -= .01f;
                    if (rage > 0)
                        rage--;
                }
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (Player.HasBuff(ModContent.BuffType<WrathBuff>()))
            {
                anger += .01f;
                if (anger > .05f)
                    rage += 1;
            }
        }
    }

    public class WrathBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.WrathSoul", MathF.Round(Main.LocalPlayer.GetModPlayer<WrathPlayer>().anger, 3),
                Main.LocalPlayer.GetModPlayer<WrathPlayer>().rage);
            base.ModifyBuffTip(ref tip, ref rare);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Sinful().SevenSoulUsed = 7;
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().anger;
            player.GetCritChance(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().rage;
            base.Update(player, ref buffIndex);
        }
    }
}
