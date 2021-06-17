using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using SagesMania;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Items.AreusDamageClass
{
    class AreusChargePack : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives 50 areus charge");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.NPCHit53;
            item.autoReuse = false;
            item.useTurn = true;
            item.consumable = true;
            item.maxStack = 30;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AreusOreItem>());
            recipe.AddTile(ModContent.TileType<AreusForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<AreusDamagePlayer>().areusResourceCurrent += 50;
            CombatText.NewText(player.Hitbox, Color.Aqua, 50);
            return true;
        }
    }
}
