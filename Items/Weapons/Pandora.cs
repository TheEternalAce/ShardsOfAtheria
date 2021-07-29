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
    public class Pandora : SpecialItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left Click to thrust a spear, <right> to fire an ice bolt\n" +
                "''Destiny of destruction awaits''");
        }

        public override void SetDefaults()
        {
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item1;
            item.damage = 107;
            item.melee = true;
            item.magic = false;
            item.mana = 0;
            item.knockBack = 6;
            item.shoot = ModContent.ProjectileType<PandoraProjectile>();
            item.shootSpeed = 2.3f;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BionicBarItem>(), 7);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddIngredient(ItemID.IceBlock, 10);
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
                item.noUseGraphic = false;
                Item.staff[item.type] = true;
                item.useTime = 20;
                item.useAnimation = 20;
                item.UseSound = SoundID.Item28;
                item.damage = 87;
                item.magic = true;
                item.melee = false;
                item.mana = 6;
                item.knockBack = 3;
                if (player.HasBuff(ModContent.BuffType<Overdrive>()))
                    item.shoot = ModContent.ProjectileType<IceBlast>();
                else
                    item.shoot = ModContent.ProjectileType<IceBolt>();
                item.shootSpeed = 15;
            }
            else
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.useTime = 30;
                item.useAnimation = 30;
                item.UseSound = SoundID.Item1;
                item.damage = 107;
                item.melee = true;
                item.magic = false;
                item.mana = 0;
                item.knockBack = 6;
                item.shoot = ModContent.ProjectileType<PandoraProjectile>();
                item.shootSpeed = 2.3f;
                return player.ownedProjectileCounts[item.shoot] < 1;
            }
            return base.CanUseItem(player);
        }
    }
}