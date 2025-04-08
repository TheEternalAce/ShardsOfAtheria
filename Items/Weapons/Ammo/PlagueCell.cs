using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class PlagueCell : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 9999;
            Item.AddDamageType(0, 6, 8);
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = 1000;
            Item.ammo = Type;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.HasItemEquipped<PlaguePack>(true) && Item.stack < 200)
            {
                Item.noGrabDelay++;
                if (Item.noGrabDelay > 300)
                {
                    Item.stack++;
                    Item.noGrabDelay = 0;
                    Item.value = 0;
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient<BionicBarItem>(5)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient(ItemID.Glass, 5)
                .AddIngredient<SoulOfSpite>(20)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}