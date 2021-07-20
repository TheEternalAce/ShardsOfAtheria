using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using SagesMania.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
    public class LivingMetal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use to toggle Megamerge\n" +
                "Must be used in hotbar");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.scale = .7f;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.Blue;
            item.buffType = ModContent.BuffType<Megamerged>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BionicBarItem>(), 15);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SMPlayer>().livingMetal = true;
        }

        public override bool UseItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                if (!player.HasBuff(ModContent.BuffType<MegamergeCooldown>()))
                {
                    player.AddBuff(ModContent.BuffType<Megamerged>(), 2);
                    CombatText.NewText(player.Hitbox, Color.White, "Megamerge!", true);
                    if (player.Male)
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MegamergeMale"), player.position);
                    else Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MegamergeFemale"), player.position);
                }
                else
                {
                    CombatText.NewText(player.Hitbox, Color.Red, "On Cooldown!");
                }
            }
            else
            {
                player.ClearBuff(ModContent.BuffType<Megamerged>());
                Main.PlaySound(SoundID.Item4, player.position);
            }
            return base.UseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
        }
    }

    public class LivingMetalHead : EquipTexture
    {
        public override bool DrawHead()
        {
            return true;
        }
    }

    public class LivingMetalBody : EquipTexture
    {
        public override bool DrawBody()
        {
            return false;
        }
    }

    public class LivingMetalLegs : EquipTexture
    {
        public override bool DrawLegs()
        {
            return false;
        }
    }
}