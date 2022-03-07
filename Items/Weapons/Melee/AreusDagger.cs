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
using ShardsOfAtheria.Projectiles.Weapon;

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
            Item.width = 48;
            Item.height = 54;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(silver: 50);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<AreusDaggerProj>();
            Item.shootSpeed = 4f;

            if (ModContent.GetInstance<Config>().areusWeaponsCostMana)
                Item.mana = 7;
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
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
        }
    }
}