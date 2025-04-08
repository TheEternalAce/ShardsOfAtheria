﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls.Extras
{
    public class VirtuousDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.consumable = true;
            Item.maxStack = 9999;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item27;

            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.immune && player.immuneTime == 0 && player.Sinful().SinfulSoulUsed > 0;
        }

        public override bool? UseItem(Player player)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was purified into nothing."), player.statLifeMax2 / 5, -player.direction);
            for (int i = 0; i < SinfulPlayer.SinfulBuffs.Length; i++)
            {
                if (player.HasBuff(SinfulPlayer.SinfulBuffs[i]))
                {
                    player.ClearBuff(SinfulPlayer.SinfulBuffs[i]);
                }
            }
            if (player.Sinful().SinfulSoulUsed != ModContent.BuffType<VirtuousSoul>())
            {
                player.AddBuff(ModContent.BuffType<VirtuousSoul>(), 1800);
                player.Sinful().SinfulSoulUsed = ModContent.BuffType<VirtuousSoul>();
            }
            else
            {
                player.Sinful().SinfulSoulUsed = 0;
            }


            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.WhiteTorch, 0, 0, 0, Color.White, 2);
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 15)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 15)
                .AddIngredient(ItemID.PurificationPowder, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
