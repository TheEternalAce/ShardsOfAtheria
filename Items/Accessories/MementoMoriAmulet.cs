using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class MementoMoriAmulet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 48;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var shards = player.Shards();
            shards.deathAmulet = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;
            var shards = player.Shards();
            object[] args = [shards.deathAmuletCharges, shards.deathInevitibility];
            string text = string.Format(this.GetLocalizedValue("Stats"), args);
            var tooltip = new TooltipLine(Mod, "MementoMoriStats", text);
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), tooltip);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.DarkShard, 2)
                .AddIngredient(ItemID.AncientBattleArmorMaterial)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
