using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public class SlothSoul : SevenSouls
    {
        public const string tip = "The lower your movement speed is, the greater your damage and defense are increased\n" +
            "While not moving damage is increased by 20% and defense by 10\n" +
            "'How very slothful of you'";

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
            player.AddBuff(ModContent.BuffType<SlothBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class SlothPlayer : ModPlayer
    {
    }

    public class SlothBuff : SevenSoulsBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sloth");
            Description.SetDefault(SlothSoul.tip);
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            SevenSoulPlayer.SevenSoulUsed = 6;
            if (player.velocity == Vector2.Zero)
            {
                player.GetDamage(DamageClass.Generic) += .2f;
                player.statDefense += 10;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) += (.15f / player.moveSpeed);
                player.statDefense += (int)(.15f / player.moveSpeed);
            }
            base.Update(player, ref buffIndex);
        }
    }
}
