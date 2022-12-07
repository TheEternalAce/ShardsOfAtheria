using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class SlothSoul : SinfulSouls
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<SlothBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class SlothBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.SlothSoul"));
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinfulPlayer>().SevenSoulUsed = 6;
            if (player.velocity == Vector2.Zero)
            {
                player.GetDamage(DamageClass.Generic) += 0.2f;
                player.statDefense += 10;
            }
            player.chilled = true;
            player.GetDamage(DamageClass.Summon) += 0.25f;
            base.Update(player, ref buffIndex);
        }
    }
}
