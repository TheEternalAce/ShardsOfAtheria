using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items
{
    class AreusChargePack : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives 50 areus charge");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.NPCHit53;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.consumable = true;
            Item.maxStack = 30;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusOreItem>())
                .AddTile(ModContent.TileType<AreusForge>())
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<SMPlayer>().areusResourceCurrent != player.GetModPlayer<SMPlayer>().areusResourceMax2)
            {
                return true;
            }
            else return false;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SMPlayer>().areusResourceCurrent += 50;
            CombatText.NewText(player.Hitbox, Color.Aqua, 50);
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SMPlayer>().areusChargePack = true;
        }
    }
}
