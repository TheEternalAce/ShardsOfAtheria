using ShardsOfAtheria.Players;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public class WrathSoul : SevenSouls
    {
        public const string tip = "Each time damage is taken damage is increased by 1%\n" +
            "Once damage bonus goes to 5% increase, crit chance starts to go up by 1% as well\n" +
            "Dying increases both by 5%\n" +
            "There is no limit to these boosts\n" +
            "Boots slowly decrease while out of combat\n" +
            "'I'm not tsundere, you're tsundere!'";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);
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
            if (Player.GetModPlayer<SoAPlayer>().inCombat == 0 && (anger > 0 || rage > 0))
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

    public class WrathBuff : SevenSoulsBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath");
            base.SetStaticDefaults();
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = WrathSoul.tip +
                $"\nCurrent damage increase: +{MathF.Round(Main.LocalPlayer.GetModPlayer<WrathPlayer>().anger, 3)}%\n" +
                $"Current critical strike chance increase: +{Main.LocalPlayer.GetModPlayer<WrathPlayer>().rage}%";
            base.ModifyBuffTip(ref tip, ref rare);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            SevenSoulPlayer.SevenSoulUsed = 7;
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().anger;
            player.GetCritChance(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().rage;
            base.Update(player, ref buffIndex);
        }
    }
}
