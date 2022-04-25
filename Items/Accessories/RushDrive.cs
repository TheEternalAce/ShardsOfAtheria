using ShardsOfAtheria.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class RushDrive : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Gives the user a 'phase 2' when below 50% life");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SoAPlayer>().rushDrive = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(SoARecipes.EvilBar, 15)
                .AddRecipeGroup(SoARecipes.EvilMaterial, 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddTile(TileID.Hellforge)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var list = ShardsOfAtheria.PhaseSwitch.GetAssignedKeys();
            string keyname = "Not bound";

            if (list.Count > 0)
            {
                keyname = list[0];
            }

            tooltips.Add(new TooltipLine(Mod, "tip", $"Press '[i:{keyname}]' to chose between two phase types:\n" +
                "Offensive: Sacrifice half of total defense for doubled damage and 5% increased crit chance\n" +
                "Defensive: Sacrifice half of total damage for doubled defense and 20% reduced damage\n" +
                "Always get 20% increased movement speed"));
        }
    }
}