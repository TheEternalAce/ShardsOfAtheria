using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public class AreusRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<AreusShard>();
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;

            Item.useTime = 24;
            Item.useAnimation = 56;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.NPCHit53;
            Item.useTurn = true;
            Item.consumable = true;

            Item.rare = ItemDefaults.RarityAreus;
            Item.value = ItemDefaults.ValueBuffPotion;
        }

        public override bool? UseItem(Player player)
        {
            player.Shards().areusRod = true;
            return true;
        }
    }
}
