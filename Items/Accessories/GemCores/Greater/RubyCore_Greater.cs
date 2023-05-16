using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class RubyCore_Greater : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RubyCore>())
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient(ItemID.FireGauntlet)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().rubyGauntlet = !hideVisual;
            player.GetDamage(DamageClass.Generic) += .1f;
            player.GetAttackSpeed(DamageClass.Generic) += .1f;
            player.GetKnockback(DamageClass.Generic) += 1;
            player.meleeScaleGlove = true;
            player.Shards().greaterRubyCore = true;
        }
    }
}