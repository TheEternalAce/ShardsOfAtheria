using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class SlothSoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<SlothBuff>();
    }

    public class SlothPlayer : ModPlayer
    {
        public bool soulActive;

        public override void ResetEffects()
        {
            soulActive = false;
        }

        public override void PreUpdate()
        {
            if (soulActive)
            {
                if (Main.time == 32400 && !Main.dayTime)
                {
                }
            }
        }
    }

    public class SlothBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.SlothSoul.Tooltip");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Sloth().soulActive = true;
            base.Update(player, ref buffIndex);
        }
    }
}
