using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;
using SagesMania.Items.Placeable;
using SagesMania.Projectiles;
using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using System.Collections.Generic;

namespace SagesMania.Items.Weapons.Melee
{
    public class AreusDagger : AreusWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A dagger with ''shocking'' potential'");
        }

        public override void SetDefaults()
        {
            item.damage = 52;
            item.melee = true;
            item.width = 32;
            item.height = 34;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.rare = ItemRarityID.Cyan;
            item.value = Item.sellPrice(silver: 50);
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<ElectricBolt>();
            item.shootSpeed = 20;

            if (!Config.areusWeaponsCostMana)
                areusResourceCost = 2;
            else item.mana = 7;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 7);
            recipe.AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(ModContent.TileType<AreusForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
                type = ModContent.ProjectileType<ElectricBlade>();
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}