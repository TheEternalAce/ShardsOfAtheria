using ShardsOfAtheria.Items.DedicatedItems.DaluyanMesses;
using ShardsOfAtheria.Items.DedicatedItems.nightlight;
using ShardsOfAtheria.Items.DedicatedItems.TheEternalAce;
using ShardsOfAtheria.Items.Weapons.Melee;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

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
        }
    }
}
