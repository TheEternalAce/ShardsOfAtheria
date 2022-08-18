using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public class SinfulArmament : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Transforms into a weapon based on your sin");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed > 0;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int weapon = 0;
                switch (player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed)
                {
                    case 1:
                        // Envy
                        weapon = ModContent.ItemType<SinfulArmament>();
                        break;
                    case 2:
                        // Gluttony
                        weapon = ModContent.ItemType<SinfulArmament>();
                        break;
                    case 3:
                        // Greed
                        weapon = ModContent.ItemType<Pantheon>();
                        break;
                    case 4:
                        // Lust
                        weapon = ModContent.ItemType<SinfulArmament>();
                        break;
                    case 5:
                        // Pride
                        weapon = ModContent.ItemType<TheAmbassador>();
                        break;
                    case 6:
                        // Sloth
                        weapon = ModContent.ItemType<SinfulArmament>();
                        break;
                    case 7:
                        // Wrath
                        weapon = ModContent.ItemType<Keteru>();
                        break;
                }
                int newItem = Item.NewItem(Item.GetSource_DropAsItem(), player.getRect(), weapon);
                Main.item[newItem].noGrabDelay = 0; // Set the new item to be able to be picked up instantly

                // Here we need to make sure the item is synced in multiplayer games.
                if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 15)
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
}
