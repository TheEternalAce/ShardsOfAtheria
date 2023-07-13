using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Entropic
{
    [AutoloadEquip(EquipType.Head)]
    public class EntropicHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 3, 50, 0);
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .06f;
            player.GetCritChance(DamageClass.Generic) += .06f;
        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<EntropicRobe>() && legs.type == ModContent.ItemType<EntropicLeggings>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.ShardsOfAtheria.SetBonus.Slayer");
            player.manaCost -= 0.1f;
            player.statManaMax2 += 40;
            player.Slayer().slayerSet = true;
            player.Slayer().maxCrystals += 6;
            player.maxMinions += 3;
            player.lifeRegen += 12;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.LunarBar, 16)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
