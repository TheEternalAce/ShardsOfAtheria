using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class LivingMetal : SpecialItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use to toggle Megamerge\n" +
                "Must be used in hotbar");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = .7f;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {  
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 15)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SMPlayer>().livingMetal = true;
            if (!player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                if (player.Male)
                    Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/MegamergeMale");
                else Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/MegamergeFemale");
            }
            else
            {
                player.ClearBuff(ModContent.BuffType<Megamerged>());
                Item.UseSound = SoundID.Item4;
            }
        }

        public override bool? UseItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                player.AddBuff(ModContent.BuffType<Megamerged>(), 2);
                CombatText.NewText(player.Hitbox, Color.White, "Megamerge!", true);
            }
            else player.ClearBuff(ModContent.BuffType<Megamerged>());
            return base.UseItem(player);
        }
    }
}