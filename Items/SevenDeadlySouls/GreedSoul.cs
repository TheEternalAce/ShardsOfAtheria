using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public class GreedSoul : SevenSouls
    {
        public const string tip = "Every gold coin in your inventory increases damage by 5% and reduces defense by 2\n" +
            "All coins disappear on death";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<GreedBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class GreedPlayer : ModPlayer
    {
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (Player.HasBuff(ModContent.BuffType<GreedBuff>()))
            {
                if (Player.HasItem(ItemID.PlatinumCoin))
                {
                    Player.inventory[Main.LocalPlayer.FindItem(ItemID.PlatinumCoin)].TurnToAir();
                }
                if (Player.HasItem(ItemID.GoldCoin))
                {
                    Player.inventory[Main.LocalPlayer.FindItem(ItemID.GoldCoin)].TurnToAir();
                }
                if (Player.HasItem(ItemID.SilverCoin))
                {
                    Player.inventory[Main.LocalPlayer.FindItem(ItemID.SilverCoin)].TurnToAir();
                }
                if (Player.HasItem(ItemID.CopperCoin))
                {
                    Player.inventory[Main.LocalPlayer.FindItem(ItemID.CopperCoin)].TurnToAir();
                }
            }
        }
    }

    public class GreedBuff : SevenSoulsBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greed");
            Description.SetDefault(GreedSoul.tip);
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed = 3;
            if (player.HasItem(ItemID.GoldCoin))
            {
                for (int i = 0; i < player.inventory[player.FindItem(ItemID.GoldCoin)].stack; i++)
                {
                    player.GetDamage(DamageClass.Generic) += .05f;
                    player.statDefense -= 2;
                }
            }
            base.Update(player, ref buffIndex);
        }
    }
}
