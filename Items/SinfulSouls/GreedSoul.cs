using ShardsOfAtheria.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class GreedSoul : SinfulSouls
    {
        public override void SetStaticDefaults()
        {
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

    public class GreedBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GreedSoul"));
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinfulPlayer>().SevenSoulUsed = 3;
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
