using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Upgrades
{
    public class RetributionBlueprints : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<FuckEarlyGameHarpies>();

        public override Item ResultItem => new(ModContent.ItemType<AreusPartisan>());

        public override int[,] Materials => new int[,]
        {
            { ModContent.ItemType<AreusShard>(), 20 },
            { ItemID.LunarBar, 14 },
        };
    }
}
