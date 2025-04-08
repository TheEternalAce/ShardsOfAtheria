using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BuffItems
{
    public class GigavoltDelicacy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.IsFood[Type] = true;

            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 38;
            Item.maxStack = 9999;

            Item.DefaultToFood(0, 0, BuffID.WellFed2, 28800);
            Item.holdStyle = ItemHoldStyleID.HoldFront;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueBuffPotion;
        }
    }
}
