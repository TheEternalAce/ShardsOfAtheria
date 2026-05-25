using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items.Consumable
{
    public class ShardOfRepentance : VirtuousItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 24;
            Item.consumable = true;
            Item.maxStack = 9999;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;

            Item.value = 250000;
        }

        public override bool CanUseItem(Player player)
        {
            return player.CardinalSoul().Sinner;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                var soul = player.CardinalSoul();
                int virtue = CardinalSoulID.ConvertToCounterpart(soul.cardinalSoul);
                player.CardinalSoul().cardinalSoul = virtue;

                for (int i = 0; i < 18; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.GoldCoin);
                }
            }
            return true;
        }
    }
}
