using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusDagger : AreusWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A dagger with 'shocking' potential'");
        }

        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 34;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(silver: 50);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ElectricBolt>();
            Item.shootSpeed = 20;

            if (!Config.areusWeaponsCostMana)
                areusResourceCost = 2;
            else Item.mana = 7;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusBarItem>(), 7)
                .AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 10)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddTile(ModContent.TileType<AreusForge>())
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(BuffID.Electrified, 600);
        }
    }
}