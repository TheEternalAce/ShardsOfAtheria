﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Mining
{
    public class RedAreusPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 54;

            Item.pick = 150;

            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.mana = 0;

            Item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item15;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.Wire, 10)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, ModContent.DustType<AreusDust>());
            }
        }
    }
}