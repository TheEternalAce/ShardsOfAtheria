using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Weapons;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.DevItems.nightlight;
using ShardsOfAtheria.Items.DevItems.DaluyanMesses;
using Terraria.GameContent.ItemDropRules;
using ShardsOfAtheria.Items.DevItems.TheEternalAce;

namespace ShardsOfAtheria.Globals
{
    public class BossBags : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (ItemID.Sets.BossBag[item.type])
            {
                LeadingConditionRule hardmodeDevSet = new LeadingConditionRule(new Conditions.IsHardmode());
                IItemDropRule aceDrop = ItemDropRule.Common(ModContent.ItemType<AceOfSpades>());
                aceDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AcesGoldFoxMask>()));
                aceDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AcesJacket>()));
                aceDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AcesPants>()));

                IItemDropRule nightDrop = ItemDropRule.Common(ModContent.ItemType<Nightlight>());
                nightDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CoatOfnight>()));

                IItemDropRule devDrop = new OneFromRulesRule(20, aceDrop, nightDrop, ItemDropRule.Common(ModContent.ItemType<StatueOfDaluyan>()));
                hardmodeDevSet.OnSuccess(devDrop);

                itemLoot.Add(hardmodeDevSet);
            }

            if (item.type == ItemID.EaterOfWorldsBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WormBloom>()));
            }
            if (item.type == ItemID.QueenBeeBossBag)
            {
                itemLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<HecateII>(), ModContent.ItemType<Glock80>()));
            }
        }
    }
}
