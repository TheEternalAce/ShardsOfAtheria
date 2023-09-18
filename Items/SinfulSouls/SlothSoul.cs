using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class SlothSoul : SinfulSouls
    {
        public override int SoulType => ModContent.BuffType<SlothBuff>();
    }

    public class SlothPlayer : ModPlayer
    {
        public bool sloth;

        public override void ResetEffects()
        {
            sloth = false;
        }
    }

    public class SlothBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Sloth().sloth = true;
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
