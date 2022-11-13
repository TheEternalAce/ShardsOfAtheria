using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ItemDropRules.Conditions;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.GrabBags
{
    public class AmmoBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 10);
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            int stackSize = 1000;

            CommonDrop[] preHardmodeAmmo = SoAGlobalItem.preHardmodeAmmo.Select((type) => new CommonDrop(type, 1, stackSize, stackSize)).ToArray();

            CommonDrop[] hardmodeAmmo = SoAGlobalItem.hardmodeAmmo.Select((type) => new CommonDrop(type, 1, stackSize, stackSize)).ToArray();

            CommonDrop[] postMLAmmo = SoAGlobalItem.postMoonLordAmmo.Select((type) => new CommonDrop(type, 1, stackSize, stackSize)).ToArray();

            OneFromRulesRule executePrehardMode = new(1, preHardmodeAmmo);

            // successfulInHardmode will resolve into successful state if we are in Hard Mode
            CommonDrop[] hardmodeDrops = preHardmodeAmmo.Concat(hardmodeAmmo).ToArray();
            LeadingConditionRule successfulInHardmode = new(new Conditions.IsHardmode());
            OneFromRulesRule executeInHardMode = new(1, hardmodeDrops);
            successfulInHardmode.OnSuccess(executeInHardMode);

            // successfulPostML will resolve into successful state if Moon Lord is dead
            CommonDrop[] postMLDrops = hardmodeDrops.Concat(postMLAmmo).ToArray();
            LeadingConditionRule successfulPostML = new(new DownedMoonLord());
            OneFromRulesRule executePostML = new(1, postMLDrops);
            successfulPostML.OnSuccess(executePostML);

            // Executes rules in defined order until one is successful. Stops once one is successful. So it tries successfulPostML, then successfulInHarmode,
            // then it finally tries chooseOnePreHardmodeDrop
            SequentialRulesRule rootRule = new(1, new IItemDropRule[] { successfulPostML, successfulInHardmode, executePrehardMode });

            itemLoot.Add(rootRule);
        }
    }
}