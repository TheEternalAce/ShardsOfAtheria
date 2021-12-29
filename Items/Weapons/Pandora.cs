using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles;
using ShardsOfAtheria.Projectiles.Weapon;
using ShardsOfAtheria.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    public class Pandora : SpecialItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left Click to thrust a spear, <right> to fire an ice bolt\n" +
                "'Destiny of destruction awaits'");
        }

        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item1;
            Item.damage = 107;
            Item.mana = 0;
            Item.knockBack = 6;
            Item.shoot = ModContent.ProjectileType<PandoraProjectile>();
            Item.shootSpeed = 2.3f;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 7)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile(ModContent.TileType<CobaltWorkbench>())
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.noMelee = true;
                Item.noUseGraphic = false;
                Item.staff[Item.type] = true;
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.UseSound = SoundID.Item28;
                Item.damage = 87;
                Item.DamageType = DamageClass.Magic;
                Item.mana = 6;
                Item.knockBack = 3;
                Item.shoot = ModContent.ProjectileType<IceBolt>();
                Item.shootSpeed = 15;
            }
            else
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.UseSound = SoundID.Item1;
                Item.damage = 107;
                Item.DamageType = DamageClass.Melee;
                Item.mana = 0;
                Item.knockBack = 6;
                Item.shoot = ModContent.ProjectileType<PandoraProjectile>();
                Item.shootSpeed = 2.3f;
                return player.ownedProjectileCounts[Item.shoot] < 1;
            }
            return base.CanUseItem(player);
        }
    }
}