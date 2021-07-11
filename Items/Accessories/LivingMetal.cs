using SagesMania.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
    public class LivingMetal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Press 'Megamerge' to toggle Megamerge\n" +
                "Works in inventory");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BionicBarItem>(), 15);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SMPlayer>().livingMetal = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
        }
    }

    public class LivingMetalHead : EquipTexture
    {
        public override bool DrawHead()
        {
            return true;
        }
    }

    public class LivingMetalBody : EquipTexture
    {
        public override bool DrawBody()
        {
            return false;
        }
    }

    public class LivingMetalLegs : EquipTexture
    {
        public override bool DrawLegs()
        {
            return false;
        }
    }
}