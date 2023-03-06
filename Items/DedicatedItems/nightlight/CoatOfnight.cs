using Terraria;
using Terraria.ID;
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
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}
