using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon;
using System.Collections.Generic;
using Terraria;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    class DecaClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Claws");
            Tooltip.SetDefault("'Claws of a Godly machine'\n" +
                "'Even the jungle tyrant fears his wrath'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2f;
            Item.crit = 100;
            Item.useTime = 1;
            Item.useAnimation = 10;
            Item.rare = ItemRarityID.Red;

            Item.autoReuse = false;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 40;
            Item.height = 40;
            Item.scale = .85f;

            Item.shoot = ModContent.ProjectileType<DecaClawProj>();
            Item.shootSpeed = 2.1f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Deca Gear", "[c/FF4100:Deca Equipment]"));
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<DecaPlayer>().modelDeca;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ModContent.ItemType<DecaShard>(), 10)
                .Register();
        }
    }
}
