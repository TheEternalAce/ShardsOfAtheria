using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
    public class LivingMetal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Press 'Megamerge' to toggle Megamerge");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddRecipeGroup("SM:SilverBars", 10);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SMPlayer>().livingMetal = true;
            if (player.extraAccessorySlots < 2)
                player.extraAccessorySlots += 1;
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