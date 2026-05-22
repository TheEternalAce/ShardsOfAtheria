using ShardsOfAtheria.Items.Armor.Areus.Guard;
using ShardsOfAtheria.Items.Armor.Areus.Imperial;
using ShardsOfAtheria.Items.Armor.Areus.Royal;
using ShardsOfAtheria.Items.Armor.Areus.Soldier;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Upgrades
{
    #region Sodlier Armor
    public class SoldierMaskBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<GuardHelmet>();

        public override Item ResultItem => new(ModContent.ItemType<SoldierMask>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.SoulofLight, 12 },
        };
    }

    public class SoldierBreastplateBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<GuardMail>();

        public override Item ResultItem => new(ModContent.ItemType<SoldierBreastplate>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.SoulofNight, 12 },
        };
    }

    public class SoldierLeggingsBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<GuardLeggings>();

        public override Item ResultItem => new(ModContent.ItemType<SoldierLeggings>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.SoulofFlight, 12 },
        };
    }
    #endregion
    #region Imperial Armor
    public class ImperialHoodBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<SoldierMask>();

        public override Item ResultItem => new(ModContent.ItemType<ImperialHood>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.BeetleHusk, 8 },
        };
    }

    public class ImperialGarmentBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<SoldierBreastplate>();

        public override Item ResultItem => new(ModContent.ItemType<ImperialGarments>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.BeetleHusk, 8 },
        };
    }

    public class ImperialGreavesBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<SoldierLeggings>();

        public override Item ResultItem => new(ModContent.ItemType<ImperialGreaves>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.BeetleHusk, 8 },
        };
    }
    #endregion
    #region Royal Armor
    public class RoyalCrownBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<ImperialHood>();

        public override Item ResultItem => new(ModContent.ItemType<RoyalCrown>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.FragmentStardust, 12 },
        };
    }

    public class RoyalJacketBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<ImperialGarments>();

        public override Item ResultItem => new(ModContent.ItemType<RoyalJacket>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.FragmentSolar, 12 },
            { ItemID.FragmentNebula, 12 },
        };
    }

    public class RoyalGreavesBlueprint : UpgradeBlueprint
    {
        public override int BaseItemType => ModContent.ItemType<ImperialGreaves>();

        public override Item ResultItem => new(ModContent.ItemType<RoyalGreaves>());

        public override int[,] Materials => new int[,]
        {
            { ItemID.FragmentVortex, 12 },
        };
    }
    #endregion
}
