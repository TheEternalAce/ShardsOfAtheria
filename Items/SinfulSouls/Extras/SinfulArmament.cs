using Microsoft.Xna.Framework;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Items.Bases;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls.Extras
{
    public class SinfulArmament : SinfulItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.consumable = true;
            Item.scale = .75f;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;

            Item.rare = ItemRarityID.Orange;
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SinfulPlayer>().SevenSoulUsed > 0;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SwitchSoulType(player, out int weapon, out string text, out Color color);
                int newItem = Item.NewItem(Item.GetSource_DropAsItem(), player.getRect(), weapon);
                Main.item[newItem].noGrabDelay = 0; // Set the new item to be able to be picked up instantly
                if (ModContent.GetInstance<ShardsClientConfig>().sinfulArmamentText)
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

        void SwitchSoulType(Player player, out int armament, out string text, out Color color)
        {
            armament = 0;
            text = "";
            color = Color.White;

            if (player.Envy().envy)
            {
                armament = 0;
                text = "";
                color = Color.Orange;
            }
            else if (player.Gluttony().gluttony)
            {
                armament = ModContent.ItemType<Gomorrah>();
                text = "I'm Gomorrah, no game can stand a chance against us!";
                color = Color.Orange;
            }
            else if (player.Greed().greed)
            {
                armament = ModContent.ItemType<Pantheon>();
                text = "My name is Pantheon, together we'll become the richest in the world!";
                color = Color.Gold;
            }
            else if (player.Lust().lust)
            {
                armament = 0;//ModContent.ItemType<SinfulArmament>();
                text = "";
                color = Color.Pink;
            }
            else if (player.Pride().pride)
            {
                armament = ModContent.ItemType<TheAmbassador>();
                text = "I am The Ambassador, the only weapon you'll ever need.";
                color = Color.White;
            }
            else if (player.Sloth().sloth)
            {
                armament = 0;//ModContent.ItemType<SinfulArmament>();
                text = "";
                color = Color.Orange;
            }
            else if (player.Wrath().wrath)
            {
                armament = ModContent.ItemType<Yamiko>();
                text = "My name is Yamiko, my edge is sharper than those pathetic blades.";
                color = Color.Red;
            }
        }
    }
}
