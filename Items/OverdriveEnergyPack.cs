using SagesMania.Tiles;
using SagesMania.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Items
{
    class OverdriveEnergyPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Refills Overdrive Time");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingUp;
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
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddTile(ModContent.TileType<AreusForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {

            if (player.GetModPlayer<SMPlayer>().overdriveTimeCurrent != player.GetModPlayer<SMPlayer>().overdriveTimeMax2 && !player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                return true;
            }
            else return false;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<SMPlayer>().overdriveTimeCurrent = 300;
            return true;
        }
    }
}
