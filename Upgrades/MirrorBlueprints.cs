using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Upgrades
{
    public class MirrorBlueprints : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<BrokenAreusMirror>();

        public override Item ResultItem => new(ModContent.ItemType<AreusMirror>());

        public override int[,] Materials => new int[,]
        {
            { ModContent.ItemType<AreusShard>(), 10 },
            { ModContent.ItemType<Jade>(), 3 },
            { ItemID.CrystalShard, 15 },
        };
    }
}
