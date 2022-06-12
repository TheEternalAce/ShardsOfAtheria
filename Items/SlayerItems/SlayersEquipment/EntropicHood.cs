using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    [AutoloadEquip(EquipType.Head)]
    public class EntropicHood : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("6% increased damage and critical strike chance");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
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
            player.setBonus = "Increases maximum mana by 40 and reduces mana usage by 10%\n" +
                "10% chance not to consume ammo\n" +
                "Increases your max number of minions by 3\n" +
                "Grants moderate life regen\n" +
                "Immunity to 'Madness'";
            player.manaCost -= 0.1f; // Reduces mana cost by 10%
            player.statManaMax2 += 40;
            player.GetModPlayer<SlayerPlayer>().slayerSet = true;
            player.maxMinions += 3;
            player.lifeRegen += 12;
            player.buffImmune[ModContent.BuffType<Madness>()] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.LunarBar, 16)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
