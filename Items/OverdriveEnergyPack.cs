using ShardsOfAtheria.Tiles;
using ShardsOfAtheria.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items
{
    class OverdriveEnergyPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Refills Overdrive Time");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.NPCHit53;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.consumable = true;
            Item.maxStack = 30;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {

            if (player.GetModPlayer<SoAPlayer>().overdriveTimeCurrent <= 0 && !player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                return true;
            }
            else return false;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SoAPlayer>().overdriveTimeCurrent = 300;
            return true;
        }
    }
}
