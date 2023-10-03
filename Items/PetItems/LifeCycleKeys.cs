using ShardsOfAtheria.Mounts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.PetItems
{
    public class LifeCycleKeys : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item79;
            Item.noMelee = true;

            Item.rare = ItemDefaults.RarityLunarPillars;
            Item.value = ItemDefaults.ValueLunarPillars;
            Item.mountType = ModContent.MountType<LifeCycle>();
        }
    }
}