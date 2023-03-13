using ShardsOfAtheria.Items.Accessories.GemCores.LesserCores;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Cores
{
    [AutoloadEquip(EquipType.Wings)]
    public class EmeraldCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(30, .5f, .5f);

            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmeraldCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ItemID.CreativeWings)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpBoost = true;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = .85f;
            ascentWhenRising = .5f;
            maxCanAscendMultiplier = 3f;
            maxAscentMultiplier = 1f;
            constantAscend = 0.135f;
        }
    }
}