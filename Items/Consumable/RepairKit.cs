using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public abstract class RepairKits : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item37;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1, 75);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SlayerPlayer>().defenseReduction > 0;
        }
    }

    public class RepairKit_Lesser : RepairKits
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().defenseReduction -= 20;
            return true;
        }
    }

    public class RepairKit : RepairKits
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().defenseReduction -= 30;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ModContent.ItemType<RepairKit_Lesser>(), 5)
                .AddIngredient(ItemID.HellstoneBar)
                .Register();
        }
    }

    public class RepairKit_Greater : RepairKit
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().defenseReduction -= 40;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ModContent.ItemType<RepairKit>(), 5)
                .AddIngredient(ItemID.HallowedBar)
                .Register();
        }
    }

    public class RepairKit_Super : RepairKit
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().defenseReduction -= 50;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RepairKit_Greater>())
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 16)
                .Register();
        }
    }
}