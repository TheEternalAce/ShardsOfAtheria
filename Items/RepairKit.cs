using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public abstract class RepairKits : ModItem
    {
        public override string Texture => "ShardsOfAtheria/Items/RepairKit";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip", "Another satisfied customer!"));
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

    public class LesserRepairKit : RepairKits
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Repairs 20 defense");
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().defenseReduction -= 20;
            return true;
        }
    }

    public class RepairKit : RepairKits
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Repairs 30 defense");
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().defenseReduction -= 30;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ModContent.ItemType<LesserRepairKit>(), 5)
                .AddIngredient(ItemID.HellstoneBar)
                .Register();
        }
    }

    public class GreaterRepairKit : RepairKit
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Repairs 40 defense");
        }

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

    public class SuperRepairKit : RepairKit
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Repairs 50 defense");
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SlayerPlayer>().defenseReduction -= 50;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<GreaterRepairKit>())
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 16)
                .Register();
        }
    }
}