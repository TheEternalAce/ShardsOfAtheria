using ShardsOfAtheria.Items.DedicatedItems.Webmillio;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Upgrades
{
    public class WarBlueprints : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<War>();

        public override Item ResultItem => new(ModContent.ItemType<War>());

        public override Func<Item, bool> CheckItem => item => (item == null || !(item.ModItem as War).upgraded);

        public override int[,] Materials => new int[,]
        {
            { ItemID.HallowedBar, 20 },
        };

        public override void ModifyItem(Item item, Player player)
        {
            (item.ModItem as War).upgraded = true;
        }
    }
}
