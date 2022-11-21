﻿using ShardsOfAtheria.Items.Bases;
using ShardsOfAtheria.Items.SinfulSouls.Extras;
using ShardsOfAtheria.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public abstract class SinfulSouls : SinfulItem
    {
        public override void SetStaticDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.useTime = Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;

            SacrificeTotal = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SevenSoul", "Only one Sinful Soul may be active at a time"));
            base.ModifyTooltips(tooltips);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SinfulPlayer>().SevenSoulUsed == 0;
        }

        public override bool? UseItem(Player player)
        {
            player.ClearBuff(ModContent.BuffType<VirtuousSoul>());
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SinfulSoul>())
                .Register();
        }
    }

    public abstract class SinfulSoulBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            base.Update(player, ref buffIndex);
        }
    }

    public class SinfulSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.maxStack = 9999;
        }
    }
}
