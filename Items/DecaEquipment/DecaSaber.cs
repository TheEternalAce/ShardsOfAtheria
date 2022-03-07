using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaSaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Saber");
            Tooltip.SetDefault("'The blade of a godly machine'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 100;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.rare = ItemRarityID.Red;

            Item.autoReuse = true;
            Item.useTurn = false;
            Item.UseSound = SoundID.Item71;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 74;
            Item.height = 74;

            Item.shoot = ModContent.ProjectileType<IonCutter>();
            Item.shootSpeed = 13f;
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