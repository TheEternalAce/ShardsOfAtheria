using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public class RighteousArmament : VirtuousItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 46;
            Item.height = 38;
            Item.consumable = true;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;

            Item.value = 250000;
        }

        public override bool CanUseItem(Player player)
        {
            return player.CardinalSoul().Saint;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SwitchSoulType(player, out int weapon, out string text, out Color color);
                int newItem = Item.NewItem(Item.GetSource_DropAsItem(), player.getRect(), weapon);
                Main.item[newItem].noGrabDelay = 0; // Set the new item to be able to be picked up instantly
                if (SoA.ClientConfig.dialogue)
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(text), color, player.whoAmI);
                }

                // Here we need to make sure the item is synced in multiplayer games.
                if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                }
            }
            return true;
        }

        static void SwitchSoulType(Player player, out int armament, out string text, out Color color)
        {
            var soul = player.CardinalSoul();
            armament = 0;
            text = "";
            color = Color.White;

            if (soul.Charity)
            {
                armament = ModContent.ItemType<RighteousArmament>();
                text = "";
                color = Color.Orange;
            }
            else if (soul.Chassity)
            {
                armament = ModContent.ItemType<RighteousArmament>();
                text = "";
                color = Color.Orange;
            }
            else if (soul.Diligence)
            {
                armament = ModContent.ItemType<RighteousArmament>();
                text = "";
                color = Color.Gold;
            }
            else if (soul.Humility)
            {
                armament = ModContent.ItemType<RighteousArmament>();
                text = "";
                color = Color.Pink;
            }
            else if (soul.Kindness)
            {
                armament = ModContent.ItemType<RighteousArmament>();
                text = "";
                color = Color.White;
            }
            else if (soul.Patience)
            {
                armament = ModContent.ItemType<RighteousArmament>();
                text = "";
                color = Color.Orange;
            }
            else if (soul.Temperance)
            {
                armament = ModContent.ItemType<Abaddon>();
                text = "";
                color = Color.Red;
            }
        }
    }
}
