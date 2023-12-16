using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public class MiniStarCritter : ModItem
    {
        public override string Texture => ModContent.GetInstance<MiniAreusStar>().Texture;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.maxStack = 9999;

            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.value = 5000;
            Item.rare = ItemDefaults.RarityAreus;
            Item.makeNPC = ModContent.NPCType<MiniAreusStar>();
        }
    }
}
