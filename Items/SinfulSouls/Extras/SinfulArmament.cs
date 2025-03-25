﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
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
        public override int RequiredSin => -1;

        public override void SetDefaults()
        {
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
            return player.GetModPlayer<SinfulPlayer>().SinfulSoulUsed > 0;
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
            else if (player.Gluttony().soulActive)
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
            else if (player.Pride().soulActive)
            {
                armament = ModContent.ItemType<Magnus>();
                text = "I am Magnus, the only weapon you'll ever need.";
                color = Color.White;
            }
            else if (player.Sloth().soulActive)
            {
                armament = 0;//ModContent.ItemType<SinfulArmament>();
                text = "";
                color = Color.Orange;
            }
            else if (player.Wrath().soulActive)
            {
                armament = ModContent.ItemType<Yamiko>();
                text = "My name is Yamiko, my edge is sharper than those pathetic blades.";
                color = Color.Red;
            }
        }
    }
}
