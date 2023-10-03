using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BuffItems
{
    public class GigavoltDelicacy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 50;
            Item.maxStack = 9999;

            Item.DefaultToPotion(BuffID.WellFed2, 8.ToMinutes());
            SoAGlobalItem.Potions.Remove(Type);

            Item.rare = ItemDefaults.RarityAreus;
            Item.value = ItemDefaults.ValueBuffPotion;
        }
    }
}
