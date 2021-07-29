using SagesMania.Buffs;
using SagesMania.Items.Placeable;
using SagesMania.Projectiles;
using SagesMania.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
    public class Prometheus : SpecialItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left Click to swing an energy scythe, <right> to throw a fireball\n" +
                "''It seems like you're worthy of playing his little game, his game of destiny!''");
        }

        public override void SetDefaults()
        {
            item.noMelee = false;
            item.noUseGraphic = false;
            item.useTime = 30;
            item.useAnimation = 30;
            item.damage = 112;
            item.melee = true;
            item.ranged = false;
            item.knockBack = 13;
            item.mana = 0;
            item.shoot = ProjectileID.None;
            item.useTurn = true;
            item.width = 54;
            item.height = 48;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BionicBarItem>(), 7);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10);
            recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.useTime = 17;
                item.useAnimation = 17;
                item.damage = 97;
                item.ranged = true;
                item.melee = false;
                item.knockBack = 6;
                item.mana = 15;
                item.UseSound = SoundID.Item20;
                if (player.HasBuff(ModContent.BuffType<Overdrive>()))
                    item.shoot = ModContent.ProjectileType<PrometheusSword>();
                else
                    item.shoot = ModContent.ProjectileType<PrometheusFire>();
                item.shootSpeed = 13f;
                item.useTurn = false;
            }
            else
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.useTime = 30;
                item.useAnimation = 30;
                item.damage = 112;
                item.melee = true;
                item.ranged = false;
                item.knockBack = 13;
                item.mana = 0;
                item.UseSound = SoundID.Item1;
                item.shoot = ProjectileID.None;
                item.useTurn = true;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                player.AddBuff(BuffID.Ichor, 10 * 60);
            }
            target.AddBuff(BuffID.OnFire, 10 * 60);
            player.AddBuff(BuffID.WeaponImbueIchor, 10 * 60);
        }
    }
}