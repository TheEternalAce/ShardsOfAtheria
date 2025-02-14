using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Upgrades
{
    public class FrostsparkUpgrade1 : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<GenesisAndRagnarok>();

        public override Item ResultItem => new(ModContent.ItemType<GenesisAndRagnarok>());

        public override Func<Player, bool> CheckPlayer => player => player.Shards().genesisRagnarockUpgrades < ToLevel;

        public virtual int ToLevel => 1;

        public override int[,] Materials => new int[,]
        {
            {ModContent.ItemType<MemoryFragment>(), 1 },
        };

        public override void ModifyItem(Item item, Player player)
        {
            var shards = player.Shards();
            if (shards.genesisRagnarockUpgrades < ToLevel)
                shards.genesisRagnarockUpgrades++;
        }
    }

    public class FrostsparkUpgrade2 : FrostsparkUpgrade1
    {
        public override int ToLevel => 2;

        public override int[,] Materials => new int[,]
        {
            { ModContent.ItemType<MemoryFragment>(), 1 },
            { ItemID.ChlorophyteBar, 14 },
        };
    }

    public class FrostsparkUpgrade3 : FrostsparkUpgrade1
    {
        public override int ToLevel => 3;

        public override int[,] Materials => new int[,]
        {
            { ModContent.ItemType<MemoryFragment>(), 1 },
            { ItemID.BeetleHusk , 16 },
        };
    }

    public class FrostsparkUpgrade4 : FrostsparkUpgrade1
    {
        public override int ToLevel => 4;

        public override int[,] Materials => new int[,]
        {
            { ModContent.ItemType<MemoryFragment>(), 1 },
            { ItemID.FragmentSolar, 18 },
        };
    }

    public class FrostsparkUpgrade5 : FrostsparkUpgrade1
    {
        public override int ToLevel => 5;

        public override int[,] Materials => new int[,]
        {
            { ModContent.ItemType<MemoryFragment>(), 1 },
            { ItemID.LunarBar, 20 },
        };
    }
}
