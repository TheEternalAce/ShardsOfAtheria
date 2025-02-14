using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class SpeedCapper : ToggleableTool
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            if (!SoA.ServerConfig.speedCapCrafting) ItemID.Sets.ShimmerTransformToItem[ItemID.Stopwatch] = Type;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.rare = ItemDefaults.RarityShimmerPermanentUpgrade;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string suffix = "";
            var key = this.GetLocalizationKey(string.Empty);
            if (mode > 0)
            {
                suffix = Language.GetOrRegister(key + "On").ToString();
            }
            else
            {
                suffix = Language.GetOrRegister(key + "Off").ToString();
            }
            tooltips[0].Text += suffix;
        }

        public override void AddRecipes()
        {
            if (SoA.ServerConfig.speedCapCrafting)
                CreateRecipe()
                    .AddIngredient(ItemID.Stopwatch)
                    .AddCondition(Condition.NearShimmer)
                    .Register();
        }
    }
}