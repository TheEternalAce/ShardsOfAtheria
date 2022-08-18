using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SevenDeadlySouls.SinfulWeapon;
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

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;
        }

        public override bool? UseItem(Player player)
        {
            int weapon = 0;
            switch (SevenSoulPlayer.SevenSoulUsed)
            {
                case 1:
                    // Envy
                    // weapon = ModContent.ItemType<>();
                    break;
                case 2:
                    // Gluttony
                    // weapon = ModContent.ItemType<>();
                    break;
                case 3:
                    // Greed
                    // weapon = ModContent.ItemType<>();
                    break;
                case 4:
                    // Lust
                    // weapon = ModContent.ItemType<>();
                    break;
                case 5:
                    // Pride
                    weapon = ModContent.ItemType<TheAmbassador>();
                    break;
                case 6:
                    // Sloth
                    // weapon = ModContent.ItemType<>();
                    break;
                case 7:
                    // Wrath
                    // weapon = ModContent.ItemType<Keteru>();
                    break;
            }
            Item.NewItem(Item.GetSource_FromThis(), player.getRect(), weapon);
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
