using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Royal
{
    [AutoloadEquip(EquipType.Body, EquipType.Back)]
    public class RoyalJacket : AreusArmorPiece
    {
        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Back}", EquipType.Back, this);
        }

        public override void SetStaticDefaults()
        {
            int capeSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Back);
            ArmorIDs.Body.Sets.IncludedCapeBack[Item.bodySlot] = capeSlot;
            ArmorIDs.Body.Sets.IncludedCapeBackFemale[Item.bodySlot] = capeSlot;
            ArmorIDs.Body.Sets.showsShouldersWhileJumping[Item.bodySlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 28;

            slotType = AreusArmorChip.SlotChest;

            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.15f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.15f;
            ArmorPlayer.areusDamage += 0.1f;
            player.statManaMax2 += 120;
            ArmorPlayer.areusBody = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(24)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.FragmentNebula, 12)
                .AddIngredient(ItemID.FragmentSolar, 12)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}
