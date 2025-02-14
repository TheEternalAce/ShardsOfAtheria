using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class WrathSoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<WrathBuff>();
    }

    public class WrathPlayer : ModPlayer
    {
        public bool wrath;
        public float anger;
        public int rage;
        public int calming;

        public override void ResetEffects()
        {
            wrath = false;
        }

        public override void PreUpdate()
        {
            if (!wrath)
            {
                anger = 0f;
                rage = 0;
            }
            if (Player.Shards().combatTimer == 0 && (anger > 0 || rage > 0))
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

        public override void OnHurt(Player.HurtInfo info)
        {
            if (wrath)
            {
                anger += .01f;
                if (anger > .05f)
                    rage += 1;
            }
        }
    }

    public class WrathBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.WrathSoul.Tooltip", MathF.Round(Main.LocalPlayer.GetModPlayer<WrathPlayer>().anger, 3),
                Main.LocalPlayer.GetModPlayer<WrathPlayer>().rage);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Wrath().wrath = true;
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().anger;
            player.GetCritChance(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().rage;
            base.Update(player, ref buffIndex);
        }
    }
}
