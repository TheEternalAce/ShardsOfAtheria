using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class AcidTrip : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(0);
            Item.AddElement(3);
            Item.AddRedemptionElement(10);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 26;
            Item.accessory = true;

            Item.damage = 42;
            Item.knockBack = 6;
            Item.crit = 2;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().acidTrip = true;
        }
    }
}