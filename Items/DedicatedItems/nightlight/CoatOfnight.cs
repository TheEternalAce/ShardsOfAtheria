using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.nightlight
{
    [AutoloadEquip(EquipType.Body)]
    public class CoatOfnight : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            ModLoader.TryGetMod("excels", out Mod excels);
            return excels == null;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }
    }
}
