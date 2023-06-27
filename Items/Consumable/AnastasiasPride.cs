using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public class AnastasiasPride : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;

            Item.useTime = 24;
            Item.useAnimation = 56;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.NPCHit53;
            Item.useTurn = true;
            Item.consumable = true;

            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 0, 20);
        }

        public override bool? UseItem(Player player)
        {
            player.Shards().anastasiaPride = true;
            string text = Language.GetTextValue("Mods.ShardsOfAtheria.Common.Anastasia");
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetworkText nText = NetworkText.FromLiteral(text);
                ChatHelper.SendChatMessageToClient(nText, Color.White, player.whoAmI);
            }
            else
            {
                Main.NewText(text);
            }
            return true;
        }
    }
}
