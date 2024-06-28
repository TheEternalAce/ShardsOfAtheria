using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class SpeedCapper : ModItem
    {
        public bool active = true;

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

        public override bool CanRightClick()
        {
            return true;
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void RightClick(Player player)
        {
            active = !active;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string mode = "";
            var key = this.GetLocalizationKey(string.Empty);
            if (active)
            {
                mode = Language.GetOrRegister(key + "On").ToString();
            }
            else
            {
                mode = Language.GetOrRegister(key + "Off").ToString();
            }
            tooltips[0].Text += mode;
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