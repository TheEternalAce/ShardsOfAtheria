using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    [AutoloadEquip(EquipType.Body)]
    public class AcesJacket : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.vanity = true;

            Item.rare = ItemRarityID.Cyan;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }
    }
}
