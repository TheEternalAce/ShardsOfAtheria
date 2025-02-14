using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Upgrades
{
    public class FlameCannonBlueprints : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<AreusRailgun>();

        public override Item ResultItem => new(ModContent.ItemType<AreusFlameCannon>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.Flamethrower, 1 },
            { ItemID.BeetleHusk, 14 },
        };
    }
}
