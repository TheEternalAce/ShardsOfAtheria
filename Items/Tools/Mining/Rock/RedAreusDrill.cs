﻿using ShardsOfAtheria.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Tools;
using Terraria.Audio;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Utilities;

namespace ShardsOfAtheria.Items.Tools.Mining.Rock
{
    public class RedAreusDrill : ModItem
    {
        public static Asset<Texture2D> glowmask;

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsDrill[Type] = true;

            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            }

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 64;

            Item.pick = 225;

            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 0;
            Item.mana = 0;

            Item.useTime = 2; //Actual Break 1 = FAST 50 = SUPER SLOW
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 32;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 75);
            Item.shoot = ModContent.ProjectileType<RedAreusDrillProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddIngredient(ItemID.Wire, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value, new Color(255, 255, 255, 50) * 0.7f, rotation, scale);
        }
    }
}