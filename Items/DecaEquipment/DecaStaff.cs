using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Barrage");
            Tooltip.SetDefault("'The staff of a godly machine'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 100;
            Item.useTime = 4;
            Item.useAnimation = 20;
            Item.reuseDelay = 22;
            Item.rare = ItemRarityID.Red;

            Item.shoot = ModContent.ProjectileType<DecaSwarmer>();
            Item.shootSpeed = 13f;
            Item.mana = 5;

            Item.noMelee = true;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 50;
            Item.height = 50;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
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
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 10)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 10)
                .AddIngredient(ModContent.ItemType<DeathEssence>())
                .Register();
        }
    }
}