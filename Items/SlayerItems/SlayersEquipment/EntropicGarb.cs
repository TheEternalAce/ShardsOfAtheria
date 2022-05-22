using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    [AutoloadEquip(EquipType.Body)]
    public class EntropicGarb : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("12% increased damage and critical strike chance");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.defense = 28;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .12f;
            player.GetCritChance(DamageClass.Generic) += .12f;
            player.moveSpeed += 0.05f; // Increase the movement speed of the player
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.LunarBar, 32)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}