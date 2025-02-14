using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Upgrades
{
    public class GauntletBlueprints : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<AreusGambit>();

        public override Item ResultItem => new(ModContent.ItemType<AreusGauntlet>());

        public override int[,] Materials => new int[,]
        {
            { ModContent.ItemType<AreusDagger>(), 1 },
            { ModContent.ItemType<AreusBow>(), 1 },
            { ModContent.ItemType<AreusKatana>(), 1 },
            { ModContent.ItemType<AreusMagnum>(), 1 },
            { ModContent.ItemType<AreusRailgun>(), 1 },
            { ItemID.BeetleHusk, 15 },
        };
    }
}
