using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class EmeraldCore_Super : ModItem
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

            Item.rare = ItemDefaults.RarityLunaticCultist;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmeraldCore_Greater>())
                .AddIngredient(ItemID.FragmentNebula, 5)
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddIngredient(ItemID.RodofDiscord)
                .AddIngredient(ItemID.BundleofBalloons)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Shards().emeraldBoots = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Frostspark Boots
            player.accRunSpeed = 6.75f;
            player.rocketBoots = 3;
            player.iceSkate = true;

            // Misc
            player.panic = true;
            player.accFlipper = true;
            player.GetJumpState(ExtraJump.CloudInABottle).Enable();
            player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
            player.GetJumpState(ExtraJump.SandstormInABottle).Enable();
            //player.hasJumpOption_Cloud = true;
            //player.hasJumpOption_Blizzard = true;
            //player.hasJumpOption_Sandstorm = true;
            player.jumpBoost = true;
            player.wingTimeMax += 15;
            player.Shards().superEmeraldCore = true;
            player.Shards().emeraldBoots = !hideVisual;
        }
    }
}